using Microsoft.AspNetCore.Mvc;
using ProjectApiBackend.Models;
using ProjectApiBackend.Services;

namespace ProjectApiBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly MessageService _messageService;

    public MessageController(MessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] MessageData data)
    {
        bool success = await _messageService.SendMessageAsync(data);
        if (!success)
        {
            return StatusCode(500, new GetResponseData
            {
                Status = false,
                Message = "Failed to send message."
            });
        }

        // ok odgovor, http 200
        return Ok(new GetResponseData
            {
                Status = true,
                Message = "Message sent."
            });
    }

    [HttpGet]
    public async Task<IActionResult> GetMessages()
    {
        List<string> messages = await _messageService.GetMessagesAsync();

        if (messages.Count != 0)
        {
            // ok odgovor, http 200
            return Ok(new GetResponseData
            { 
                Status = true, 
                Message = "Message received.", 
                NumericalMessages =  messages,
            });
        }
        else
        {
           return StatusCode(500, new GetResponseData
            { 
                Status = false, 
                Message = "No message received.", 
            }); 
        }
    }
}
