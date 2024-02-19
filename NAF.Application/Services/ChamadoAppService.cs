using NAF.Application.Interfaces;
using NAF.Domain.Entities;
using NAF.Domain.Enum;
using NAF.Domain.Interface.Repositories;
using NAF.Domain.Interface.Services;
using NAF.Domain.Requests;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Transactions;

namespace NAF.Application.Services
{
    public class ChamadoAppService : IChamadoAppService
    {
        private readonly IChamadoService _chamadoService;
        private readonly IUserAppService _userAppService;
        private readonly IServicoAppService _servicoAppService;
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IChamadoComentarioRepository _chamadoComentarioRepository;
        private readonly IChamadoDocumentoRepository _chamadoDocumentoRepository;
        private readonly IChamadoHistoricoRepository _chamadoHistoricoRepository;

        public ChamadoAppService(IChamadoService chamadoService,
                                 IUserAppService userAppService,
                                 IServicoAppService servicoAppService,
                                 IChamadoRepository chamadoRepository,
                                 IChamadoComentarioRepository chamadoComentarioRepository,
                                 IChamadoDocumentoRepository chamadoDocumentoRepository,
                                 IChamadoHistoricoRepository chamadoHistoricoRepository)
        {
            _chamadoService = chamadoService;
            _userAppService = userAppService;
            _servicoAppService = servicoAppService;
            _chamadoRepository = chamadoRepository;
            _chamadoComentarioRepository = chamadoComentarioRepository;
            _chamadoDocumentoRepository = chamadoDocumentoRepository;
            _chamadoHistoricoRepository = chamadoHistoricoRepository;
        }

        public Chamado CreateChamado(CreateChamadoRequest request)
        {
            var entity = new Chamado
            {
                Codigo = Guid.NewGuid(),
                CodigoUsuario = request.CodigoUsuario,
                CodigoServico = request.CodigoServico,
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                DtInclusao = DateTime.Now,
                Situacao = 0
            };

            _chamadoService.ValidateChamado(entity);

            using var ts = new TransactionScope();

            if (request.Arquivos != null && request.Arquivos?.Count > 0)
            {
                foreach (var arquivo in request.Arquivos)
                {
                    var chamadoDocumentoRequest = new FileUploadRequest
                    {
                        CodigoUsuario = request.CodigoUsuario,
                        File = arquivo
                    };
                    CreateChamadoDocumento(chamadoDocumentoRequest, entity.Codigo);
                }
            }

            _chamadoRepository.Insert(entity);
            _chamadoRepository.SaveChanges();

            CreateChamadoHistorico(TipoAlteracaoEnum.ChamadoCriado, entity.Codigo, entity.CodigoUsuario, "Chamado Entity", null, JsonConvert.SerializeObject(entity));

            ts.Complete();

            return entity;
        }

        public List<Chamado> GetAllChamado(Guid codigoUsuario)
        {
            var usuario = _userAppService.GetUserByCodigo(codigoUsuario);

            List<Chamado> chamados = usuario.TipoPerfil switch
            {
                TipoPerfil.Comunidade => _chamadoRepository.GetAll().Where(o => o.CodigoUsuario.Equals(codigoUsuario)).ToList(),
                TipoPerfil.Aluno => _chamadoRepository.GetAll().Where(o => o.CodigoOperador.Equals(codigoUsuario)).ToList(),
                TipoPerfil.Professor => _chamadoRepository.GetAll().ToList(),
                _ => throw new ArgumentOutOfRangeException("Tipo de perfil não cadastrado no sistema."),
            };

            Dictionary<Guid, Usuario> chamadoUsuarios = new Dictionary<Guid, Usuario>();
            Dictionary<Guid, Servico> chamadoServicos = new Dictionary<Guid, Servico>();
            foreach (var chamado in chamados)
            {
                if (chamado.CodigoOperador.HasValue && chamado.CodigoOperador != Guid.Empty)
                {
                    if (chamadoUsuarios.ContainsKey(chamado.CodigoOperador.GetValueOrDefault()))
                        chamado.Operador = chamadoUsuarios[chamado.CodigoOperador.GetValueOrDefault()];
                    else
                    {
                        chamado.Operador = _userAppService.GetUserByCodigo(chamado.CodigoOperador.GetValueOrDefault());
                        chamadoUsuarios.Add(chamado.CodigoOperador.GetValueOrDefault(), chamado.Operador);
                    }
                }

                if (chamado.CodigoUsuario != Guid.Empty)
                {
                    if (chamadoUsuarios.ContainsKey(chamado.CodigoUsuario))
                        chamado.Usuario = chamadoUsuarios[chamado.CodigoUsuario];
                    else
                    {
                        chamado.Usuario = _userAppService.GetUserByCodigo(chamado.CodigoUsuario);
                        chamadoUsuarios.Add(chamado.CodigoUsuario, chamado.Usuario);
                    }
                }

                if (chamado.CodigoServico != Guid.Empty)
                {
                    if (chamadoServicos.ContainsKey(chamado.CodigoServico))
                        chamado.Servico = chamadoServicos[chamado.CodigoServico];
                    else
                    {
                        chamado.Servico = _servicoAppService.GetServico(chamado.CodigoServico);
                        chamadoServicos.Add(chamado.CodigoServico, chamado.Servico);
                    }
                }
            }

            return chamados;
        }

        public Chamado GetChamado(Guid id)
        {
            var chamado = _chamadoRepository.GetByCodigo(id);

            if (chamado is null)
                throw new KeyNotFoundException($"Chamado com o codigo {id} não foi encontrada.");

            if (chamado.CodigoOperador.HasValue && chamado.CodigoOperador != Guid.Empty)
                chamado.Operador = _userAppService.GetUserByCodigo(chamado.CodigoOperador.GetValueOrDefault());

            if (chamado.CodigoUsuario != Guid.Empty)
                chamado.Usuario = _userAppService.GetUserByCodigo(chamado.CodigoUsuario);

            if (chamado.CodigoServico != Guid.Empty)
                chamado.Servico = _servicoAppService.GetServico(chamado.CodigoServico);

            return chamado;
        }

        public void UpdateChamado(UpdateChamadoRequest request, Guid codigoOperador, Guid id)
        {
            Chamado entity = GetChamado(id);
            Chamado chamado = entity;

            entity.CodigoOperador = request.CodigoOperador;

            _chamadoService.ValidateChamado(entity);

            entity.DtAlteracao = DateTime.Now;

            using var ts = new TransactionScope();

            _chamadoRepository.Update(entity);
            _chamadoRepository.SaveChanges();

            CreateChamadoHistorico(TipoAlteracaoEnum.ChamadoAlterado, id, codigoOperador, "Chamado Entity", JsonConvert.SerializeObject(chamado), JsonConvert.SerializeObject(entity));

            ts.Complete();
        }

        public void UpdateChamadoSituacao(UpdateChamadoSituacaoRequest request, Guid codigoUsuario, Guid id)
        {
            Chamado entity = GetChamado(id);
            Chamado chamado = entity;

            entity.Situacao = request.Situacao;

            _chamadoService.ValidateChamado(entity);

            entity.DtAlteracao = DateTime.Now;

            using var ts = new TransactionScope();

            _chamadoRepository.Update(entity);
            _chamadoRepository.SaveChanges();

            CreateChamadoHistorico(TipoAlteracaoEnum.ChamadoAlterado, id, codigoUsuario, "Chamado Entity", JsonConvert.SerializeObject(chamado), JsonConvert.SerializeObject(entity));

            ts.Complete();
        }

        public void DeleteChamado(Guid id, Guid codigoUsuario)
        {
            var entity = GetChamado(id);

            using var ts = new TransactionScope();

            _chamadoRepository.Remove(entity);

            CreateChamadoHistorico(TipoAlteracaoEnum.ChamadoDeletado, id, codigoUsuario, "Chamado Entity", JsonConvert.SerializeObject(entity), null);

            ts.Complete();
        }

        public ChamadoDocumento CreateChamadoDocumento(FileUploadRequest request, Guid id)
        {
            using var binaryReader = new BinaryReader(request.File.OpenReadStream(), Encoding.BigEndianUnicode);
            var file = binaryReader.ReadBytes((int)request.File.Length);

            var entity = new ChamadoDocumento
            {
                Codigo = Guid.NewGuid(),
                CodigoUsuario = request.CodigoUsuario,
                CodigoChamado = id,
                Arquivo = file,
                NomeArquivo = request.File.FileName.Trim(),
                DtInclusao = DateTime.Now
            };

            _chamadoService.ValidateChamadoDocumento(entity);

            using var ts = new TransactionScope();

            _chamadoDocumentoRepository.Insert(entity);
            _chamadoDocumentoRepository.SaveChanges();

            CreateChamadoHistorico(TipoAlteracaoEnum.ChamadoDocumentoCriado, entity.Codigo, entity.CodigoUsuario, "Chamado Documento Entity", null, JsonConvert.SerializeObject(entity));

            ts.Complete();

            return entity;
        }

        public List<ChamadoDocumento> GetAllChamadoDocumento(Guid chamadoId)
        {
            var documentos = _chamadoDocumentoRepository.GetAll().Where(o => o.CodigoChamado.Equals(chamadoId)).ToList();
            if (documentos is null || documentos.Count == 0)
                throw new Exception("Não foi encontrado documentos para esse chamado");

            Dictionary<Guid, Chamado> chamados = new Dictionary<Guid, Chamado>();
            Dictionary<Guid, Usuario> documentoUsuarios = new Dictionary<Guid, Usuario>();

            foreach (var documento in documentos)
            {
                if (documento.CodigoUsuario != Guid.Empty)
                {
                    if (documentoUsuarios.ContainsKey(documento.CodigoUsuario))
                        documento.Usuario = documentoUsuarios[documento.CodigoUsuario];
                    else
                    {
                        documento.Usuario = _userAppService.GetUserByCodigo(documento.CodigoUsuario);
                        documentoUsuarios.Add(documento.CodigoUsuario, documento.Usuario);
                    }
                }

                if (documento.CodigoChamado != Guid.Empty)
                {
                    if (documentoUsuarios.ContainsKey(documento.CodigoChamado))
                        documento.Chamado = chamados[documento.CodigoChamado];
                    else
                    {
                        documento.Chamado = GetChamado(documento.CodigoChamado);
                        chamados.Add(documento.CodigoChamado, documento.Chamado);
                    }
                }
            }
            return documentos;
        }

        public ChamadoDocumento GetChamadoDocumento(Guid chamadoId, Guid documentoId)
        {
            var documento = _chamadoDocumentoRepository.GetByCodigo(documentoId);
            if (documento is null)
                throw new Exception("O documento não foi encontrado");
            if (!documento.CodigoChamado.Equals(chamadoId))
                throw new Exception("O documento solicitado não pertence ao chamado informado");

            if (documento.CodigoUsuario != Guid.Empty)
                documento.Usuario = _userAppService.GetUserByCodigo(documento.CodigoUsuario);

            if (documento.CodigoChamado != Guid.Empty)
                documento.Chamado = GetChamado(documento.CodigoChamado);

            return documento;
        }

        public void DeleteChamadoDocumento(Guid chamadoId, Guid documentoId, Guid codigoUsuario)
        {
            var documento = GetChamadoDocumento(chamadoId, documentoId);

            using var ts = new TransactionScope();

            _chamadoDocumentoRepository.Remove(documento);

            CreateChamadoHistorico(TipoAlteracaoEnum.ChamadoDocumentoDeletado, chamadoId, codigoUsuario, "Chamado Documento Entity", JsonConvert.SerializeObject(documento), null);

            ts.Complete();
        }

        public ChamadoComentario CreateChamadoComentario(CreateChamadoComentarioRequest request, Guid id)
        {
            var entity = new ChamadoComentario
            {
                Codigo = Guid.NewGuid(),
                CodigoUsuario = request.CodigoUsuario,
                CodigoChamado = id,
                Mensagem = request.Mensagem,
                DtInclusao = DateTime.Now
            };

            _chamadoService.ValidateChamadoComentario(entity);

            using var ts = new TransactionScope();

            _chamadoComentarioRepository.Insert(entity);
            _chamadoComentarioRepository.SaveChanges();

            CreateChamadoHistorico(TipoAlteracaoEnum.ChamadoComentarioCriado, id, entity.CodigoUsuario, "Chamado Comentario Entity", null, JsonConvert.SerializeObject(entity));

            ts.Complete();

            return entity;
        }

        public List<ChamadoComentario> GetAllChamadoComentario(Guid chamadoId)
        {
            var comentarios = _chamadoComentarioRepository.GetAll().Where(o => o.CodigoChamado.Equals(chamadoId)).ToList();
            if (comentarios is null || !comentarios.Any())
                throw new Exception("Não foram encontrados comentários para esse chamado");

            Dictionary<Guid, Chamado> chamados = new Dictionary<Guid, Chamado>();
            Dictionary<Guid, Usuario> comentarioUsuarios = new Dictionary<Guid, Usuario>();

            foreach (var comentario in comentarios)
            {
                if (comentario.CodigoUsuario != Guid.Empty)
                {
                    if (comentarioUsuarios.ContainsKey(comentario.CodigoUsuario))
                        comentario.Usuario = comentarioUsuarios[comentario.CodigoUsuario];
                    else
                    {
                        comentario.Usuario = _userAppService.GetUserByCodigo(comentario.CodigoUsuario);
                        comentarioUsuarios.Add(comentario.CodigoUsuario, comentario.Usuario);
                    }
                }

                if (comentario.CodigoChamado != Guid.Empty)
                {
                    if (comentarioUsuarios.ContainsKey(comentario.CodigoChamado))
                        comentario.Chamado = chamados[comentario.CodigoChamado];
                    else
                    {
                        comentario.Chamado = GetChamado(comentario.CodigoChamado);
                        chamados.Add(comentario.CodigoChamado, comentario.Chamado);
                    }
                }
            }
            return comentarios;
        }

        public ChamadoComentario GetChamadoComentario(Guid chamadoId, Guid comentarioId)
        {
            var comentario = _chamadoComentarioRepository.GetByCodigo(comentarioId);
            if (comentario is null)
                throw new Exception("O comentario não foi encontrado");

            if (!comentario.CodigoChamado.Equals(chamadoId))
                throw new Exception("O comentario solicitado não pertence ao chamado informado");

            if (comentario.CodigoUsuario != Guid.Empty)
                comentario.Usuario = _userAppService.GetUserByCodigo(comentario.CodigoUsuario);

            if (comentario.CodigoChamado != Guid.Empty)
                comentario.Chamado = GetChamado(comentario.CodigoChamado);

            return comentario;
        }

        public void UpdateChamadoComentario(Guid chamadoId, Guid comentarioId, UpdateChamadoComentarioRequest request)
        {
            var entity = GetChamadoComentario(chamadoId, comentarioId);
            var comentario = entity;

            entity.Mensagem = request.Mensagem;

            _chamadoService.ValidateChamadoComentario(entity);

            entity.DtAlteracao = DateTime.Now;

            using var ts = new TransactionScope();

            _chamadoComentarioRepository.Update(entity);
            _chamadoComentarioRepository.SaveChanges();

            CreateChamadoHistorico(TipoAlteracaoEnum.ChamadoComentarioAlterado, chamadoId, request.CodigoUsuario, "Chamado Comentario Entity", JsonConvert.SerializeObject(comentario), JsonConvert.SerializeObject(entity));

            ts.Complete();
        }

        public void DeleteChamadoComentario(Guid chamadoId, Guid comentarioId, Guid codigoUsuario)
        {
            var comentario = GetChamadoComentario(chamadoId, comentarioId);

            using var ts = new TransactionScope();

            _chamadoComentarioRepository.Remove(comentario);

            CreateChamadoHistorico(TipoAlteracaoEnum.ChamadoComentarioDeletado, chamadoId, codigoUsuario, "Chamado Comentario Entity", JsonConvert.SerializeObject(comentario), null);

            ts.Complete();
        }

        public List<ChamadoHistorico> GetAllChamadoHistorico(Guid id)
        {
            var chamadoHistorico = _chamadoHistoricoRepository.GetAll().Where(o => o.CodigoChamado.Equals(id)).ToList();
            if (chamadoHistorico is null || !chamadoHistorico.Any())
                throw new Exception("Não foram encontrados ações para esse chamado");

            Dictionary<Guid, Chamado> chamados = new Dictionary<Guid, Chamado>();
            Dictionary<Guid, Usuario> chamadoUsuarios = new Dictionary<Guid, Usuario>();

            foreach (var item in chamadoHistorico)
            {
                if (item.CodigoUsuario != Guid.Empty)
                {
                    if (chamadoUsuarios.ContainsKey(item.CodigoUsuario))
                        item.Usuario = chamadoUsuarios[item.CodigoUsuario];
                    else
                    {
                        item.Usuario = _userAppService.GetUserByCodigo(item.CodigoUsuario);
                        chamadoUsuarios.Add(item.CodigoUsuario, item.Usuario);
                    }
                }

                if (item.CodigoChamado != Guid.Empty)
                {
                    if (chamados.ContainsKey(item.CodigoChamado))
                        item.Chamado = chamados[item.CodigoChamado];
                    else
                    {
                        item.Chamado = GetChamado(item.CodigoChamado);
                        chamados.Add(item.CodigoChamado, item.Chamado);
                    }
                }
            }

            return chamadoHistorico;
        }

        private void CreateChamadoHistorico(TipoAlteracaoEnum tipoAlteracao, Guid codigoChamado, Guid codigoUsuario, string campoAlterado, string? valorAntigo, string? valorNovo)
        {
            var entity = new ChamadoHistorico
            {
                Codigo = Guid.NewGuid(),
                CodigoChamado = codigoChamado,
                CodigoUsuario = codigoUsuario,
                TipoAlteracao = (int)tipoAlteracao,
                CampoAlterado = campoAlterado,
                ValorAntigo = valorAntigo,
                ValorNovo = valorNovo
            };

            _chamadoHistoricoRepository.Insert(entity);
            _chamadoHistoricoRepository.SaveChanges();
        }

        public List<dynamic> GetAllChamadoOperador(Guid operadorId)
        {
            var chamados = _chamadoRepository.GetAll().Where(x => x.CodigoOperador == operadorId).ToList();

            var listResponse = new List<dynamic>();

            foreach (var chamado in chamados)
            {
                var servico = _servicoAppService.GetServico(chamado.CodigoServico);
                var usuario = _userAppService.GetUserByCodigo(chamado.CodigoUsuario);

                listResponse.Add(
                    new
                    {
                        CodigoChamado = chamado.Codigo,
                        chamado.Titulo,
                        servico.HoraComplementar,
                        NomeUsuario = usuario.Nome
                    });
            }

            return listResponse;
        }
    }
}
