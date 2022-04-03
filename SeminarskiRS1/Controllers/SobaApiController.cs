using Data.EF;
using Data.EFModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeminarskiRS1.ViewModels;
using System.Linq;

namespace SeminarskiRS1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SobaApiController : ControllerBase
    {
        private MojDbContext _dbContext;
        public SobaApiController(MojDbContext db)
        {
            _dbContext = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_dbContext.Soba.ToList());
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_dbContext.Soba.FirstOrDefault(a=>a.SobaId==id));
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetTipove")]
        public IActionResult GetTipove()
        {
            try
            {
                return Ok(_dbContext.SobaTip.ToList());
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post(SobaEvidentirajVM vm)
        {
            try
            {
                var soba = new Soba()
                {
                    BrojSobe = vm.BrojSobe,
                    NazivSobe = vm.NazivSobe,
                    SobaTipID = vm.SobaTipID,
                    OpisSobe = vm.OpisSobe,
                    Cijena = vm.Cijena
                };
                _dbContext.Soba.Add(soba);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult Put(SobaEvidentirajVM vm)
        {
            try
            { 
                var soba= _dbContext.Soba.FirstOrDefault(a=>a.SobaId == vm.SobaId);
                if(soba!=null)
                {
                    soba.BrojSobe = vm.BrojSobe;
                    soba.NazivSobe = vm.NazivSobe;
                    soba.SobaTipID = vm.SobaTipID;
                    soba.OpisSobe = vm.OpisSobe;
                    soba.Cijena = vm.Cijena;
                }
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var soba = _dbContext.Soba.FirstOrDefault(a => a.SobaId ==id);
                if (soba != null)
                {
                    _dbContext.Soba.Remove(soba);
                    _dbContext.SaveChanges();
                    
                }
                return Ok();
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }
    }
}

                      
               
