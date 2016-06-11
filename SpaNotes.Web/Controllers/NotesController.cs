using SpaNotes.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using SpaNotes.Web.Models;
using System.Threading.Tasks;
using SpaNotes.Entities;
using AutoMapper;

namespace SpaNotes.Web.Controllers
{
    [Authorize]
    [RoutePrefix("api/notes")]
    public class NotesController : ApiController
    {
        #region Fields
        private INoteService _noteService;
        #endregion

        #region Constructors
        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }
        #endregion

        #region Actions
        // GET: api/notes
        public async Task<IHttpActionResult> Get()
        {
            IEnumerable<Note> notes =  await Task<IEnumerable<Note>>.Run(() => 
            {
                return _noteService.GetAllNotes();
            });

            IEnumerable<NoteDto> noteDtos = Mapper.Map<IEnumerable<Note>, IEnumerable<NoteDto>>(notes);

            return Ok(noteDtos);
        }

        // GET: api/notes/details/1
        [Route("details/{id:int}")]
        public IHttpActionResult Get(int? id)
        {
            if (id == null)
                return BadRequest();

            Note note = _noteService.GetNoteById(id.Value);

            if (note == null)
                return BadRequest();

            NoteDto noteDto = Mapper.Map<Note, NoteDto>(note);

            return Ok(noteDto);
        }

        // POST: api/notes/update/1
        [HttpPost]
        [Route("update/{id:int}")]
        public async Task<IHttpActionResult> Update([FromBody]NoteBindingModel noteBindingModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Note note = Mapper.Map<NoteBindingModel, Note>(noteBindingModel);

            if (_noteService.UpdateNote(note))
            {
                await _noteService.CommitAsync();

                return Ok();
            }

            return BadRequest();
        }

        // POST: api/notes/add
        [HttpPost]
        [Route("add")]
        public async Task<IHttpActionResult> Add([FromBody]NoteBindingModel noteBindingModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Note note = Mapper.Map<NoteBindingModel, Note>(noteBindingModel);

            _noteService.AddNote(note);
            await _noteService.CommitAsync();

            return Ok();
        }

        // POST: api/notes/delete/1
        [HttpPost]
        [Route("delete/{id:int}")]
        public async Task<IHttpActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            if (_noteService.RemoveNoteById(id.Value))
            {
                await _noteService.CommitAsync();

                return Ok();
            }

            return BadRequest();
        }
        #endregion
    }
}
