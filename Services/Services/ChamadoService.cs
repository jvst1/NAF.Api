using NAF.Domain.Entities;
using NAF.Domain.Interface.Services;

namespace NAF.Domain.Services.Services
{
    public class ChamadoService : IChamadoService
    {
        public ChamadoService()
        {
        }

        public void ValidateChamado(Chamado chamado)
        {
            if (string.IsNullOrEmpty(chamado.Titulo))
                throw new ArgumentException("O titulo do chamado é obrigatório.");

            if (!string.IsNullOrEmpty(chamado.Titulo) && chamado.Titulo.Length > 255)
                throw new ArgumentException("O titulo do chamado não pode conter mais que 255 caracteres.");

            if (chamado.CodigoServico.Equals(Guid.Empty))
                throw new ArgumentException("É necessário informar o tipo de serviço solicitado.");

            if (string.IsNullOrEmpty(chamado.Descricao))
                throw new ArgumentException("É necessário adicionar uma breve descrição ao chamado.");
        }

        public void ValidateChamadoDocumento(ChamadoDocumento documento)
        {
            if (documento.CodigoChamado.Equals(Guid.Empty))
                throw new ArgumentException("O upload do arquivo deve ser feito em um chamado. O código do chamado não foi encontrado.");

            if (documento.CodigoUsuario.Equals(Guid.Empty))
                throw new ArgumentException("O upload do arquivo deve ser feito por um usuário. Não foi possível identificar quem tentou fazer o upload do arquivo.");

            if (documento.Arquivo is null)
                throw new ArgumentException("O é necessário que seja anexado um arquivo.");

            if (documento.Arquivo != null && documento.Arquivo.Length <= 0)
                throw new ArgumentException("O arquivo anexado é inválido.");
        }

        public void ValidateChamadoComentario(ChamadoComentario comentario)
        {
            if (comentario.CodigoChamado.Equals(Guid.Empty))
                throw new ArgumentException("O comentario deve ser feito em um chamado. O código do chamado não foi encontrado.");

            if (comentario.CodigoUsuario.Equals(Guid.Empty))
                throw new ArgumentException("O comentario deve ser feito por um usuário. Não foi possível identificar quem tentou fazer o comentario.");

            if (string.IsNullOrEmpty(comentario.Mensagem))
                throw new ArgumentException("O comentario deve conter uma mensagem, não é possível enviar um comentário em branco.");
        }
    }
}
