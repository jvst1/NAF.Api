using NAF.Application.Interfaces;
using NAF.Domain.Entities;
using NAF.Domain.Interface.Repositories;
using NAF.Domain.Interface.Services;
using NAF.Domain.Requests;
using System.Text;

namespace NAF.Application.Services
{
    public class ChamadoAppService : IChamadoAppService
    {
        private readonly IChamadoService _chamadoService;
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IChamadoComentarioRepository _chamadoComentarioRepository;
        private readonly IChamadoDocumentoRepository _chamadoDocumentoRepository;

        public ChamadoAppService(IChamadoService chamadoService, IChamadoRepository chamadoRepository, IChamadoComentarioRepository chamadoComentarioRepository, IChamadoDocumentoRepository chamadoDocumentoRepository)
        {
            _chamadoService = chamadoService;
            _chamadoRepository = chamadoRepository;
            _chamadoComentarioRepository = chamadoComentarioRepository;
            _chamadoDocumentoRepository = chamadoDocumentoRepository;
        }
        
        public void CreateChamado(CreateChamadoRequest request)
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

            _chamadoRepository.Insert(entity);
            _chamadoRepository.SaveChanges();
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
            var entity = GetChamado(request.Codigo);

            entity.Titulo = request.Titulo;
            entity.Descricao = request.Descricao;

            _chamadoService.ValidateChamado(entity);

            entity.DtAlteracao = DateTime.Now;

            _chamadoRepository.Update(entity);
            _chamadoRepository.SaveChanges();
        }

        public void UpdateChamadoSituacao(UpdateChamadoSituacaoRequest request)
        {
            var entity = GetChamado(request.Codigo);

            entity.Titulo = request.Titulo;
            entity.Situacao = request.Situacao;

            _chamadoService.ValidateChamado(entity);

            entity.DtAlteracao = DateTime.Now;

            _chamadoRepository.Update(entity);
            _chamadoRepository.SaveChanges();
        }

        public void DeleteChamado(Guid id)
        {
            var entity = GetChamado(id);
            _chamadoRepository.Remove(entity);
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

            _chamadoDocumentoRepository.Insert(entity);
            _chamadoDocumentoRepository.SaveChanges();
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

        public void DeleteChamadoDocumento(Guid chamadoId, Guid documentoId)
        {
            var documento = GetChamadoDocumento(chamadoId, documentoId);
            _chamadoDocumentoRepository.Remove(documento);
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

            _chamadoComentarioRepository.Insert(entity);
            _chamadoComentarioRepository.SaveChanges();
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

            entity.Mensagem = request.Mensagem;

            _chamadoService.ValidateChamadoComentario(entity);

            entity.DtAlteracao = DateTime.Now;

            _chamadoComentarioRepository.Update(entity);
            _chamadoComentarioRepository.SaveChanges();
        }

        public void DeleteChamadoComentario(Guid chamadoId, Guid documentoId)
        {
            var comentario = GetChamadoComentario(chamadoId, documentoId);
            _chamadoComentarioRepository.Remove(comentario);
        }
    }
}
