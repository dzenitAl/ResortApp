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
    public class BungalovApiController : ControllerBase
    {
        private MojDbContext _dbContext;
        public BungalovApiController(MojDbContext db)
        {
            _dbContext = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_dbContext.Bungalov.ToList());
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
                return Ok(_dbContext.Bungalov.FirstOrDefault(a => a.BungalovId == id));
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetTipove")]
        public IActionResult GetTipove( )
        {
            try
            {
                return Ok(_dbContext.BungalovTip.ToList());
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post(BungalovEvidentirajVM vm)
        {
            try
            {
                var bungalov = new Bungalov()
                {
                   BrojBungalova=vm.BrojBungalova,
                   NazivBungalova=vm.NazivBungalova,
                   BungalovTipID=vm.BungalovTipID,
                   Cijena=vm.Cijena,
                   OpisBungalova=vm.OpisBungalova
                };
                _dbContext.Bungalov.Add(bungalov);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult Put(BungalovEvidentirajVM vm)
        {
            try
            {
                var bungalov = _dbContext.Bungalov.FirstOrDefault(a => a.BungalovId==vm.BungalovId);
                if (bungalov != null)
                {
                    bungalov.BrojBungalova = vm.BrojBungalova;
                    bungalov.NazivBungalova = vm.NazivBungalova;
                    bungalov.BungalovTipID = vm.BungalovTipID;
                    bungalov.Cijena=vm.Cijena;
                    bungalov.OpisBungalova = vm.OpisBungalova;
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
                var bungalov = _dbContext.Bungalov.FirstOrDefault(a => a.BungalovId == id);
                if (bungalov != null)
                {
                    _dbContext.Bungalov.Remove(bungalov);
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

