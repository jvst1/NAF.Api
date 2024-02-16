using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NAF.Application.Interfaces;
using NAF.Domain.Enum;
using NAF.Domain.Requests;

namespace NAF.Api.Controllers
{
    public class AreaController : NafControllerBase
    {
        private readonly IAreaAppService _areaService;

        public AreaController(IAreaAppService areaService, IUserAppService userAppService) : base(userAppService)
        {
            _areaService = areaService;
        }

        [HttpPost]
        [Authorize(Roles = nameof(TipoPerfil.Professor))]
        public ActionResult CreateArea(CreateAreaRequest request)
        {
            try
            {
                var response = _areaService.CreateArea(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetAllArea()
        {
            try
            {
                var area = _areaService.GetAllArea();

                if (area is null || area?.Count == 0)
                    return NoContent();

                return Ok(area);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetArea(Guid id)
        {
            try
            {
                var area = _areaService.GetArea(id);
                return Ok(area);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(TipoPerfil.Professor))]
        public ActionResult UpdateArea([FromBody] UpdateAreaRequest request, [FromRoute] Guid id)
        {
            try
            {
                if (!id.Equals(request.Codigo))
                    return BadRequest("O código informado não é o mesmo.");

                _areaService.UpdateArea(request);
                return Ok("Area atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(TipoPerfil.Professor))]
        public ActionResult DeleteArea([FromRoute] Guid id)
        {
            try
            {
                _areaService.DeleteArea(id);
                return Ok("Area apagada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}