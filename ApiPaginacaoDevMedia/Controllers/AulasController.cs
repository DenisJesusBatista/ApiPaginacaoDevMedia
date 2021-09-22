using ApiPaginacaoDevMedia.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        #region DeleteAula
        public IHttpActionResult DeleteAula(int idCurso, int ordemAula)
        {
            //Localiza o curso
            var curso = db.Cursos.Find(idCurso);

            //404 - Curso não encontrado
            if (curso == null)
                return NotFound();

            //Localiza a aula pela ordem
            var aula = curso.Aulas.FirstOrDefault(a => a.Ordem == ordemAula);

            //404 - Aula não encontrada
            if (aula == null)
                return NotFound();

            //Remove a aula
            db.Entry(aula).State = EntityState.Deleted;

            //Se removeu uma aula do meio, desloca as próximas para cima (reduz a ordem )
            curso.Aulas.Where(a => a.Ordem > ordemAula)
                .ToList()
                .ForEach(a => a.Ordem--);

            //Grava as alterações
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }
        #endregion

        #region PostAula        
        public IHttpActionResult PostAula(int idCurso, Aula aula)
        {
            // Localiza o curso
            var curso = db.Cursos.Find(idCurso);

            //404 - Curso não encontrado
            if (curso == null)
                return NotFound();

            //400 - Dados inválidos
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Calcula a ordem da próxima aula a ser cadastrada ( última + 1 )
            int proximaAula = curso.Aulas.Max(a => a.Ordem) + 1;

            //Ordem inválida: adiciona no final
            if (aula.Ordem > proximaAula)
                aula.Ordem = proximaAula;

            //Inserindo a aula no meio: desloca as demais aulas para baixo (aumenta a ordem)
            else if (aula.Ordem < proximaAula)
                curso.Aulas.Where(a => a.Ordem >= aula.Ordem)
                    .ToList()
                    .ForEach(a => a.Ordem++);

            //Adiciona a nova aula
            curso.Aulas.Add(aula);

            //Grava as alterações
            db.SaveChanges();

            return CreatedAtRoute("Aulas", new { idCurso = idCurso, ordemAula = aula.Ordem }, aula);
        }

        #endregion
    }
}
