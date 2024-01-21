using Microsoft.AspNetCore.Mvc;
using NAF.Application.Interfaces;
using NAF.Domain.Requests;

namespace NAF.Api.Controllers
{
    public class AreaController : NafControllerBase
    {
        private readonly IAreaAppService _areaService;

        public AreaController(IAreaAppService areaService)
        {
            _areaService = areaService;
        }

        [HttpPost]
        public ActionResult CreateArea(CreateAreaRequest request)
        {
            try
            {
                _areaService.CreateArea(request);

                return Ok("Area criada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
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