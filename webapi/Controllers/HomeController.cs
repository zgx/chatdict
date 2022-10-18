using Microsoft.AspNetCore.Mvc;

namespace chatdict.webapi.Controllers;

[ApiController]
[Route("/")]
public class HomeController: ControllerBase
{
    public class Website
    {
        public string Name { get; } = "chatdict";
        public string Version { get; } = "v1";
    }
    
    [HttpGet]
    public Website Get()
    {
        return new Website();
    }
}