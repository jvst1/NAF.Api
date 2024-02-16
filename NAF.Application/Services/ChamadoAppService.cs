using NAF.Application.Interfaces;
using NAF.Domain.Entities;
using NAF.Domain.Enum;
using NAF.Domain.Interface.Repositories;
using NAF.Domain.Interface.Services;
using NAF.Domain.Requests;
using Newtonsoft.Json;
using System.Text;
using System.Transactions;

namespace NAF.Application.Services
{
    public class ChamadoAppService : IChamadoAppService
    {
        private readonly IChamadoService _chamadoService;
        private readonly IUserAppService _userAppService;
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IChamadoComentarioRepository _chamadoComentarioRepository;
        private readonly IChamadoDocumentoRepository _chamadoDocumentoRepository;
        private readonly IChamadoHistoricoRepository _chamadoHistoricoRepository;

        public ChamadoAppService(IChamadoService chamadoService,
                                 IUserAppService userAppService,
                                 IChamadoRepository chamadoRepository,
                                 IChamadoComentarioRepository chamadoComentarioRepository,
                                 IChamadoDocumentoRepository chamadoDocumentoRepository,
                                 IChamadoHistoricoRepository chamadoHistoricoRepository)
        {
            _chamadoService = chamadoService;
            _userAppService = userAppService;
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

            _chamadoRepository.Insert(entity);
            _chamadoRepository.SaveChanges();

            CreateChamadoHistorico(TipoAlteracaoEnum.ChamadoCriado, entity.Codigo, entity.CodigoUsuario, "Chamado Entity", null, JsonConvert.SerializeObject(entity));

            ts.Complete();

            return entity;
        }

        public List<Chamado> GetAllChamado(Guid codigoUsuario)
        {
            var usuario = _userAppService.GetUserByCodigo(codigoUsuario);

            return usuario.TipoPerfil switch
            {
                TipoPerfil.Comunidade => _chamadoRepository.GetAll().Where(o => o.CodigoUsuario.Equals(codigoUsuario)).ToList(),
                TipoPerfil.Aluno => _chamadoRepository.GetAll().Where(o => o.CodigoOperador.Equals(codigoUsuario)).ToList(),
                TipoPerfil.Professor => _chamadoRepository.GetAll().ToList(),
                _ => throw new ArgumentOutOfRangeException("Tipo de perfil não cadastrado no sistema."),
            };
        }

        public Chamado GetChamado(Guid id)
        {
            var chamado = _chamadoRepository.GetByCodigo(id);

            if (chamado is null)
                throw new KeyNotFoundException($"Chamado com o codigo {id} não foi encontrada.");

            return chamado;
        }

        public void UpdateChamado(UpdateChamadoRequest request)
        {
            Chamado entity = GetChamado(request.Codigo);
            Chamado chamado = entity;

            entity.Titulo = request.Titulo;
            entity.Descricao = request.Descricao;

            _chamadoService.ValidateChamado(entity);

            entity.DtAlteracao = DateTime.Now;

            using var ts = new TransactionScope();

            _chamadoRepository.Update(entity);
            _chamadoRepository.SaveChanges();

            CreateChamadoHistorico(TipoAlteracaoEnum.ChamadoAlterado, request.Codigo, request.CodigoUsuario, "Chamado Entity", JsonConvert.SerializeObject(chamado), JsonConvert.SerializeObject(entity));

            ts.Complete();
        }

        public void UpdateChamadoSituacao(UpdateChamadoSituacaoRequest request)
        {
            Chamado entity = GetChamado(request.Codigo);
            Chamado chamado = entity;

            entity.Titulo = request.Titulo;
            entity.Situacao = request.Situacao;

            _chamadoService.ValidateChamado(entity);

            entity.DtAlteracao = DateTime.Now;

            using var ts = new TransactionScope();

            _chamadoRepository.Update(entity);
            _chamadoRepository.SaveChanges();

            CreateChamadoHistorico(TipoAlteracaoEnum.ChamadoAlterado, request.Codigo, request.CodigoUsuario, "Chamado Entity", JsonConvert.SerializeObject(chamado), JsonConvert.SerializeObject(entity));

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

        public void CreateChamadoDocumento(FileUploadRequest request, Guid id)
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
        }

        public List<ChamadoDocumento> GetAllChamadoDocumento(Guid chamadoId)
        {
            var documentos = _chamadoDocumentoRepository.GetAll().Where(o => o.CodigoChamado.Equals(chamadoId)).ToList();
            if (documentos is null || documentos.Count == 0)
                throw new Exception("Não foi encontrado documentos para esse chamado");

            return documentos;
        }

        public ChamadoDocumento GetChamadoDocumento(Guid chamadoId, Guid documentoId)
        {
            var documento = _chamadoDocumentoRepository.GetByCodigo(documentoId);
            if (documento is null)
                throw new Exception("O documento não foi encontrado");
            if (!documento.CodigoChamado.Equals(chamadoId))
                throw new Exception("O documento solicitado não pertence ao chamado informado");

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

        public void CreateChamadoComentario(CreateChamadoComentarioRequest request, Guid id)
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
        }

        public List<ChamadoComentario> GetAllChamadoComentario(Guid chamadoId)
        {
            var comentarios = _chamadoComentarioRepository.GetAll().Where(o => o.CodigoChamado.Equals(chamadoId)).OrderByDescending(o => o.Id).ToList();
            if (comentarios is null || comentarios.Count == 0)
                throw new Exception("Não foram encontrados comentários para esse chamado");

            return comentarios;
        }

        public ChamadoComentario GetChamadoComentario(Guid chamadoId, Guid comentarioId)
        {
            var comentario = _chamadoComentarioRepository.GetByCodigo(comentarioId);
            if (comentario is null)
                throw new Exception("O comentario não foi encontrado");

            if (!comentario.CodigoChamado.Equals(chamadoId))
                throw new Exception("O comentario solicitado não pertence ao chamado informado");

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
            var historicoAcoes = _chamadoHistoricoRepository.GetAll().Where(o => o.CodigoChamado.Equals(id)).OrderByDescending(o => o.Id).ToList();
            if (historicoAcoes is null || historicoAcoes.Count == 0)
                throw new Exception("Não foram encontrados ações para esse chamado");

            return historicoAcoes;
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
    }
}
