﻿using Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.BusinessLL
{
    public class FuncionarioBll : ICRUD<Funcionario>
    {
        public Response Insert(Funcionario item)
        {
            throw new NotImplementedException();
        }

        public Response Update(Funcionario item)
        {
            throw new NotImplementedException();
        }
        public Response Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DataResponse<Funcionario> GetAll()
        {
            throw new NotImplementedException();
        }

        public SingleResponse<Funcionario> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public SingleResponse<Funcionario> GetByID(int id)
        {
            throw new NotImplementedException();
        }

    }
}
