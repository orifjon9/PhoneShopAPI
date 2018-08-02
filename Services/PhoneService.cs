
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PhoneShopAPI.Data.Access.DAL;
using PhoneShopAPI.Models;
using PhoneShopAPI.Services.Interfaces;
using PhoneShopAPI.ViewModels;

namespace PhoneShopAPI.Services
{
    public class PhoneService : IPhoneService
    {
        private readonly IPhoneRepository _repository;
        private readonly IMapper _mapper;

        public PhoneService(IMapper mapper, IPhoneRepository repository)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<PhoneViewModel> GetPhoneAsync(int id)
        {
            return _mapper
                .Map<Phone, PhoneViewModel>(
                    await _repository.GetByIdAsync(id));
        }

        public IEnumerable<PhoneViewModel> ListPhones()
        {
            return _mapper
                .Map<IEnumerable<Phone>, IEnumerable<PhoneViewModel>>(
                    _repository.GetAll());
        }

        public async Task<bool> CreatePhoneItemAsync(PhoneViewModel phoneToCreate)
        {
            try
            {
                var phone = _mapper.Map<PhoneViewModel, Phone>(phoneToCreate);
                await _repository.AddAsync(phone);
                await _repository.CommitAsync();

                phoneToCreate.Id = phone.Id;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeletePhoneItem(PhoneViewModel phoneToDelete)
        {
            var phone = _mapper.Map<PhoneViewModel, Phone>(phoneToDelete);
            _repository.Delete(phone);
            await _repository.CommitAsync();
            return true;
        }

        public async Task<bool> UpdatePhoneItem(int id, PhoneViewModel phoneToUpdate)
        {
            try
            {
                var phone = _mapper.Map<PhoneViewModel, Phone>(phoneToUpdate);
                phone.Id = id;
                
                _repository.Update(phone);
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