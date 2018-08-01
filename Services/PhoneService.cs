
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PhoneShopAPI.Data.Access.DAL;
using PhoneShopAPI.Models;
using PhoneShopAPI.Services.Interfaces;

namespace PhoneShopAPI.Services
{
    public class PhoneService : IPhoneService
    {
        private readonly IPhoneRepository _repository;
        private readonly ModelStateDictionary _modelState;

        public PhoneService(ModelStateDictionary modelState, IPhoneRepository repository)
        {
            this._repository = repository;
            this._modelState = modelState;
        }

        public bool ValidatePhone(Phone phoneToValidate)
        {
            if (phoneToValidate.Name.Trim().Length == 0)
            {
                _modelState.AddModelError("Name", "Name is required!");
            }
            if (phoneToValidate.Description.Trim().Length == 0)
            {
                _modelState.AddModelError("Description", "Description is required!");
            }
            return _modelState.IsValid;
        }

        public async Task<Phone> GetPhoneAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public IEnumerable<Phone> ListPhones()
        {
            return _repository.GetAll();
        }

        public async Task<bool> CreatePhoneItemAsync(Phone phoneToCreate)
        {
            if (!ValidatePhone(phoneToCreate))
            {
                return false;
            }
            try
            {
                await _repository.AddAsync(phoneToCreate);
                await _repository.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeletePhoneItem(Phone phoneToDelete)
        {
            _repository.Delete(phoneToDelete);
            await _repository.CommitAsync();
            return true;
        }

        public async Task<bool> UpdatePhoneItem(Phone srcPhone, Phone phoneToUpdate)
        {
            if (!ValidatePhone(phoneToUpdate))
            {
                return false;
            }
            try
            {
                srcPhone.Name = phoneToUpdate.Name;
                srcPhone.Description = phoneToUpdate.Description;
                await _repository.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}