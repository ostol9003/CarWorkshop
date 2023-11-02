using AutoMapper;
using CarWorkshop.Application.CarWorkshop.Commands.CReateCarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop;
using CarWorkshop.Application.CarWorkshop.Queries.GetAllCarWorkshops;
using CarWorkshop.Application.CarWorkshop.Queries.GetCarWorkshopByEncodedName;
using CarWorkshop.MVC.Extensions;
using CarWorkshop.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarWorkshop.MVC.Controllers
{
    public class CarWorkshopController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #region Constructor

        public CarWorkshopController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        #endregion


        #region Index

        public async Task<IActionResult> Index()
        {
            var carWorkshops = await _mediator.Send(new GetAllCarWorkshopsQuery());
            return View(carWorkshops);
        }

        #endregion

        #region Create

        [Authorize(Roles = "Owner,Moderator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Owner,Moderator")]
        public async Task<IActionResult> Create(CreateCarWorkshopCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);
          this.SetNotification("success",$"Created carworkshop: {command.Name}");
            
            return RedirectToAction(nameof(Index)); 
        }

        #endregion

        #region Edit

        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));
            if (!dto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            EditCarWorkshopCommand model = _mapper.Map<EditCarWorkshopCommand>(dto);
            return View(model);
        }


        [HttpPost]
        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName, EditCarWorkshopCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Details

        [Route("CarWorkshop/{encodedName}/Details")]
        public async Task<IActionResult> Deatils(string encodedName)
        {
            var dto = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));
            return View(dto);
        }

        #endregion
    }
}