using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Controllers
{
    [ApiController]
    [Route("api/security")]

    public class SecurityController:ControllerBase
    {
        private readonly IDataProtector protector;

        public SecurityController(IDataProtectionProvider protectionProvider)
        {
            protector = protectionProvider.CreateProtector("hidden_value_secret_and_unique");
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            string plainText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                "Curabitur porttitor accumsan elit, accumsan malesuada turpis efficitur sit amet. " +
                "Praesent congue tristique dolor a vestibulum. Suspendisse ut accumsan lacus. " +
                "Vestibulum neque ex, semper non aliquam nec, sagittis et dui. Maecenas eget massa nibh. " +
                "Suspendisse quis egestas urna. Pellentesque a neque lacus. " +
                "Vivamus interdum, metus eget fermentum pulvinar, erat purus sodales sem, sit amet tincidunt nulla ligula vel turpis. " +
                "Mauris at auctor est, vel gravida risus. Cras hendrerit tempus massa ut maximus. " +
                "Morbi semper hendrerit turpis. Etiam varius nisl nec vehicula feugiat. In hac habitasse platea dictumst. " +
                "Mauris gravida elementum erat nec vehicula. Vestibulum fringilla dapibus risus in facilisis. " +
                "Etiam ut libero vitae justo luctus feugiat quis sed elit. Cras pretium nisl facilisis pellentesque tempus. ";
            string encryptedText = protector.Protect(plainText);
            string decryptedText = protector.Unprotect(encryptedText);

            return Ok(new { plainText, encryptedText, decryptedText });
        }
    }

}
