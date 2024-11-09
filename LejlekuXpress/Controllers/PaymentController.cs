using LejlekuXpress.Data;
using LejlekuXpress.Data.DTO;
using LejlekuXpress.Data.ServiceInterfaces;
using LejlekuXpress.Models;
using LejlekuXpress.Services;
using Microsoft.AspNetCore.Mvc;

namespace LejlekuXpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentService _service;
        private readonly LogService _logService;
        public PaymentController(IPaymentService service, LogService logService)
        {
            _service = service;
            _logService = logService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPayment(PaymentDTO request)
        {
            try
            {
                var payment = await _service.AddPayment(request);

                await _logService.CreateLog(new Log
                {
                    Action = "AddPayment",
                    Message = "Payment made successfully"
                });

                return Ok(payment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetPayment(int UserId)
        {
            try
            {
                var result = await _service.GetPayment(UserId);
                if (result == null || result.Count == 0)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = _service.DeletePayment(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, PaymentDTO request)
        {
            try
            {
                var result = _service.UpdatePayment(id, request);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
