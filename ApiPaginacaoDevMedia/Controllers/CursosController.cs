using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiPaginacaoDevMedia.Models;

namespace ApiPaginacaoDevMedia.Controllers
{
    public class CursosController : ApiController
    {
        private DevMediaContext db = new DevMediaContext();

        public IHttpActionResult PostCurso(Curso curso)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Cursos.Add(curso);
            db.SaveChanges();

            //Retornar um código 201 (Created-http) 
            return CreatedAtRoute("DefaultApi", new { id = curso.Id }, curso);
        }

        public IHttpActionResult GetCurso(int id)
        {
            if (id <= 0)
                return BadRequest("O id deve ser um número maior que zero.");

            var curso = db.Cursos.Find(id);

            if (curso == null)
                return NotFound();


            return Ok(curso);
        }

        public IHttpActionResult PutCurso(int id, Curso curso)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != curso.Id)
                return BadRequest("O id informado na URL é diferente do id informado no corpo da requisição.");

            if (db.Cursos.Count(c => c.Id == id) == 0)
                return NotFound();

            db.Entry(curso).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult DeleteCurso(int id)
        {
            if (id <= 0)
                return BadRequest("O id deve ser um número maior que zero.");

            var curso = db.Cursos.Find(id);

            if (curso == null)
                return NotFound();

            db.Cursos.Remove(curso);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);

        }

        public IHttpActionResult GetCursos()
        {
            var cursos = db.Cursos.OrderBy(c => c.DataPublicacao); //Expressão Lambda

            return Ok(cursos);
        }
        

    }
}