using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //PicController is for rendering ONE image when the client asks for it in Url using an id
    //CatalogController will render all Catalog Items including the images. The HTML's image 
    //Control can be bound to the PictureUrl which has the API links will call PicController to 
    //render the images.
    public class PicController : ControllerBase
    {
        //create a global variable so it can be accessed from the GetImage method
        private readonly IWebHostEnvironment _env;

        //When the project is deployed in a VM/Docker container we don't know the 
        //location of the folder if its a G drive or C drive etc. So, to get to
        //the wwwroot, we can let the Startup.cs inject the environment to us,
        //When the Startup starts up the project it knows the environment you are on.
        //In Constructor we ask for the env to be injected.

        public PicController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // HttpGet: Client is asking data from the server. Client gives all the info 
        //as part of the URL. Client is not sending data in body of the request.
        //HttpPost: Client is sending data as part of body of request i.e. in a form.
        //HttpDelete: Delete some content
        //IActionResult Action = Request that user sends, Result = JSON or Webpage Output

        //Define subroutes
        [HttpGet("{id}")]
        public IActionResult GetImage(int id)
        {
            //get the Path to Webroot folder from the environment
            var webRoot = _env.WebRootPath;
            //generate a path to the pic 
            var path = Path.Combine($"{webRoot}/Pics/", $"Yoga{id}.jpg");
            //read the content of the file
            var buffer=System.IO.File.ReadAllBytes(path);
            //render the content (image) instead of the filepath because Client's environment
            //varies from that of the Server 
            return File(buffer, "image/jpeg");
        }
    }
}
