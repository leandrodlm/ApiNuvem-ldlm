using Microsoft.AspNetCore.Mvc;
using APIQueueCommunication.Models;
using APIQueueCommunication.Services;

namespace ApiNuvem-ldlm.Controllers
{
    [Route("api/[controller]")]
    public class MensagensNaFilaController : Controller
    {
        private QueueService queueManager;

        public MessageQueueController()
        {
            queueManager = new QueueService();
        }

        // GET: api/messagequeue
        [HttpGet]
        public IActionResult list()
        {
            return Ok("Not implemented!\n\nThis application receives only requests via posts.");
        }

        // GET api/messagequeue/5
        [HttpGet("{id}")]
        public IActionResult findOne(int id)
        {
            return Ok("Not implemented!\n\nThis application receives only requests via posts.");
        }

        // POST api/messagequeue
        [HttpPost]
        public IActionResult create([FromBody] MessageQueue messageQueue)
        {
            if (null == messageQueue || null == messageQueue.message)
            {
                return BadRequest("You didn't send a message in JSON format\nLook this example:\n\n{message: \"This is a test message\"}");
            }
            else if (messageQueue.message.Equals(""))
            {
                return BadRequest("Your message is empty! Please, provide some content.");
            }
            else
            {
                queueManager.sendMessage(messageQueue);
                return Ok("The message \"" + messageQueue.message + "\" has been processed successfully!");
            }
        }
}
}
