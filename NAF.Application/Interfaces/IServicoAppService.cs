﻿using NAF.Domain.Entities;
using NAF.Domain.Requests;

namespace NAF.Application.Interfaces
{
    public interface IServicoAppService
    {
        void CreateServico(CreateServicoRequest request);
        Servico GetServico(Guid id);
        void UpdateServico(UpdateServicoRequest request);
        void DeleteServico(Guid id);
    }
}