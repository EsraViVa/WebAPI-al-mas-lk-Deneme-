using Microsoft.AspNetCore.Mvc;
using ToDoWebAPI.Context;
using ToDoWebAPI.Entities;
using ToDoWebAPI.Models;

namespace ToDoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : Controller
    {
        //GET Kayıt veya kayıtları çekmek 
        //POST Yeni bir kayıt eklemek.
        //PUT Bir kaydı tamamen güncellemek.
        //PATCH Bir kaydın bazı propertylerini güncellemek.
        //DELETE Bir kaydı silmek.


        // StatusCode //
        // 200 Ok (success)
        // 201 Created 
        // 400 hatalı istek
        // 401 giriş yapılmış login olmamış kullanıcı,
        // 403 giriş yapılmış yetkisiz kullanıcı 
        // 404 veri bulunamadı / action bulunamadı

        // 500 server error 

        private readonly ToDoContext _db;
        public ToDosController(ToDoContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult AddToDo(AddToDoRequest request)
        {
            var entity = new ToDoEntity()
            {
                Title = request.Title,
                Content = request.Content,
                IsDone = false,
            };
            _db.ToDos.Add(entity);

            try
            {
                _db.SaveChanges();
                return Ok(); //return StatusCode(200)
            }
            catch (Exception)
            {

                return StatusCode(500);
            }

        }
        [HttpPut]
        public IActionResult UpdateToDo(int id, UpdateToDoRequest request)
        {
            var entity = _db.ToDos.Find(id);

            entity.Title = request.Title;
            entity.Content = request.Content;
            entity.IsDone = request.IsDone;

            _db.ToDos.Update(entity);

            try
            {
                _db.SaveChanges();
                return Ok(); //return StatusCode(200)
            }
            catch (Exception)
            {

                return StatusCode(500);
            }

        }
        [HttpPatch("{id}")]
        public IActionResult CheckToDo(int id, CheckToDoRequest request)
        {
            var entity = _db.ToDos.Find(id);

            entity.IsDone = request.State;

            _db.ToDos.Update(entity);

            try
            {
                _db.SaveChanges();
                return Ok(); //return StatusCode(200)
            }
            catch (Exception)
            {

                return StatusCode(500);
            }

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteToDo(int id)
        {
            var entity = _db.ToDos.Find(id);

            if (entity == null) //olmayan bir veriyi silmek istiyorsak
            {
                return BadRequest();   // return StatusCode(400);
            }

            _db.ToDos.Remove(entity); //HARD DELETE 

            try
            {
                _db.SaveChanges();
                return Ok(); //return StatusCode(200)
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult GetAllToDos()
        {
            var entities = _db.ToDos.ToList();

            return Ok(entities);
        }
        [HttpGet("{id}")]
        public IActionResult GetTodo(int id)
        {
            var entity = _db.ToDos.Find(id);

            if(entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
    }
}
