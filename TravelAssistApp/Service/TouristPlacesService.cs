using System;
using System.Collections.Generic;
using TravelAssistApp.Infrastructure;
using TravelAssistApp.Models;
using TravelAssistApp.Repository;

namespace TravelAssistApp.Service
{
    public interface ITouristPlacesService
    {
        IEnumerable<TouristPlace> GetAllTouristPlaces();
        bool AddTouristPlace(TouristPlace touristPlace);
        bool UpdateTouristPlace(TouristPlace touristPlace);
        TouristPlace GetTouristPlaceDetailsById(int touristPlaceId);
        bool DeleteTouristPlace(TouristPlace touristPlace);
        bool SaveRecord();
    }

    public class TouristPlacesService : ITouristPlacesService
    {
        private readonly ITouristPlacesRepository _touristPlacesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TouristPlacesService(ITouristPlacesRepository touristPlacesRepository, IUnitOfWork unitOfWork)
        {
            _touristPlacesRepository = touristPlacesRepository;
            _unitOfWork = unitOfWork;
        }

        public bool AddTouristPlace(TouristPlace touristPlace)
        {
            try
            {
                _touristPlacesRepository.Add(touristPlace);
                SaveRecord();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool DeleteTouristPlace(TouristPlace touristPlace)
        {
            try
            {
                _touristPlacesRepository.Delete(touristPlace);
                SaveRecord();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public IEnumerable<TouristPlace> GetAllTouristPlaces()
        {
            var allTouristPlaces = _touristPlacesRepository.GetAll();
            return allTouristPlaces;
        }

        public TouristPlace GetTouristPlaceDetailsById(int touristPlaceId)
        {
            var placeDetails = _touristPlacesRepository.GetById(touristPlaceId);
            return placeDetails;
        }

        public bool SaveRecord()
        {
            try
            {
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool UpdateTouristPlace(TouristPlace touristPlace)
        {
            try
            {
                _touristPlacesRepository.Update(touristPlace);
                SaveRecord();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }
    }
}