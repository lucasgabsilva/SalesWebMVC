using System.Collections.Generic;
using System.Linq;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;


namespace SalesWebMvc.Services

{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;


        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }
        //insere o objeto no banco de dados
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
        //buscando um vendedor por id
        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }
        //realizando ações com o id encontrado

        //removendo do banco de dados
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        //atualizando dados de um vendedor
        public void Update(Seller obj) 
        { 
            //busca no banco um valor x cujo id seja igual ao id do objeto 
            if(!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

        }
    }
}

