// File:    IHospitalRepository.cs
// Author:  Vlajkov
// Created: Sunday, May 24, 2020 12:39:28 AM
// Purpose: Definition of Interface IHospitalRepository

using Model.Users;
using System;

namespace Repository.GeneralRepository
{
   public interface IHospitalRepository : IRepository<Hospital,int>
   {
   }
}