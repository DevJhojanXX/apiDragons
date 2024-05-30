using Animals.Context;
using Animals.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Threading.Tasks.Dataflow;

namespace apiDragons.Controllers
{
    [Route("api/[controller]")]
    public class SpeciesTypeController : Controller
    {
        private AnimalContext _context;

        public SpeciesTypeController(AnimalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<SpeciesType> Get() => _context.Species.ToList();

        [HttpPost]
        public ActionResult Add(SpeciesType stype)
        {
            SpeciesType speciesType;
            if (ModelState.IsValid) // Verifica si el modelo es válido
            {
                // Crea una nueva instancia de MiEntidad y asigna los valores del modelo
                speciesType = new SpeciesType()
                {
                     type = stype.type,
                };

                _context.Species.Add(speciesType); // Agrega la entidad al contexto
                _context.SaveChanges(); // Guarda los cambios en la base de datos

            }
            else
            {
                return BadRequest("El modelo No es valido");
            }
            return Ok(speciesType);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var specie = _context.Species.FirstOrDefault(e => e.id == id);
            if (specie == null)
            {
                return BadRequest("This property cant´n null");
            }
            _context.Species.Remove(specie);
            _context.SaveChanges();
            return Ok($"Species {specie.type} Delete");
        }
    }
}
