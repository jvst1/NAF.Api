﻿using NAF.Domain.Base.Entity;

namespace NAF.Domain.Entities
{
    public class Chamado : EntityBase
    {
        public Guid CodigoUsuario { get; set; }
        public Guid? CodigoOperador { get; set; }
        public Guid CodigoServico { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int Situacao { get; set; }
        public DateTime DtAlteracao { get; set; }
    }
}
