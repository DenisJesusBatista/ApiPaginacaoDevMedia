using ApiPaginacaoDevMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiPaginacaoDevMedia.Controllers
{
    public class AulasController : ApiController
    {
        private DevMediaContext db = new DevMediaContext();

        public IHttpActionResult GetAulas(int idCurso)
        {
            //Localiza o curso
            var curso = db.Cursos.Find(idCurso);

            //404 - Curso não encontrado
            if (curso == null)
                return NotFound();

            //return Ok("Ok");


            return Ok(curso.Aulas.OrderBy(a => a.Ordem).ToList());


        }
    }
}
