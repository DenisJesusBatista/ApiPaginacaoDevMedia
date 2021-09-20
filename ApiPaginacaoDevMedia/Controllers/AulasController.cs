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

        #region GetAulas      
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
        #endregion

        #region GetAula
        public IHttpActionResult GetAula(int idCurso, int ordemAula)
        {
            //Localiza o curso
            var curso = db.Cursos.Find(idCurso);

            //404 - Curso não encontrado
            if (curso == null)
                return NotFound();

            //Localiza a aula

            var aula = curso.Aulas.FirstOrDefault(a => a.Ordem == ordemAula);

            //404 - Aula não encontrada
            if (aula == null)
                return NotFound();

            return Ok(aula);
        }
        #endregion
    }
}
