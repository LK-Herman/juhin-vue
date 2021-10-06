using JuhinAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class RootController : ControllerBase
    {
        /// <summary>
        /// Gets the endpoints links
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "getRoot")]
        public ActionResult<IEnumerable<Link>> Get()
        {
            List<Link> links = new List<Link>();

            links.Add(new Link(href: Url.Link("getRoute", new { }), rel: "self", method: "GET"));
            links.Add(new Link(href: Url.Link("getUsers", new { }), rel: "self", method: "GET"));
            links.Add(new Link(href: Url.Link("getRoles", new { }), rel: "self", method: "GET"));
            links.Add(new Link(href: Url.Link("assignRole", new { }), rel: "assign-role", method: "POST"));
            links.Add(new Link(href: Url.Link("removeRole", new { }), rel: "remove-role", method: "POST"));
            links.Add(new Link(href: Url.Link("createAccount", new { }), rel: "create-account", method: "POST"));
            links.Add(new Link(href: Url.Link("loginUser", new { }), rel: "login", method: "POST"));
            links.Add(new Link(href: Url.Link("renewToken", new { }), rel: "renew-token", method: "POST"));

            return links;
        }
    }
}
