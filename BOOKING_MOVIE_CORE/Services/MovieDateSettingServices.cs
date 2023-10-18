using System;
using System.Collections.Generic;
using System.Linq;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class MovieDateSettingServices : ApplicationService<MovieDateSetting>
    {
        private readonly MovieCinemaServices _movieCinema;
        private readonly MovieRoomServices _movieRoom;
        private readonly MovieTimeSettingServices _movieTimeSetting;

        public MovieDateSettingServices(
            MovieCinemaServices movieCinema,
            MovieRoomServices movieRoom,
            MovieTimeSettingServices movieTimeSetting,
            GenericDomainService<MovieDateSetting> domainService
            ) : base(domainService)
        {
            _movieCinema = movieCinema;
            _movieRoom = movieRoom;
            _movieTimeSetting = movieTimeSetting;
        }

        public void CreateMovieDateSettings(List<MovieDateSetting> movieDateSettings, string currentUserEmail)
        {
            var movieDateSettingsToCreate = new List<MovieDateSetting>();
            var movieCinemasToCreate = new List<MovieCinema>();
            var movieRoomsToCreate = new List<MovieRoom>();
            var movieTimeSettingsToCreate = new List<MovieTimeSetting>();

            foreach (var movieDateSetting in movieDateSettings)
            {
                var createMovieDateSetting = new MovieDateSetting()
                {
                    Created = DateTime.Now,
                    CreatedBy = currentUserEmail,
                    Status = OBJECT_STATUS.ENABLE,
                    Time = movieDateSetting.Time
                };
                movieDateSettingsToCreate.Add(createMovieDateSetting);

                foreach (var movieCinema in movieDateSetting.MovieCinemas.ToList())
                {
                    var createMovieCinema = new MovieCinema()
                    {
                        Created = DateTime.Now,
                        CreatedBy = currentUserEmail,
                        Status = OBJECT_STATUS.ENABLE,
                        CinemaId = createMovieDateSetting.Id,
                    };
                    movieCinemasToCreate.Add(createMovieCinema);

                    foreach (var movieRoom in movieCinema.MovieRooms)
                    {
                        var createMovieCinemaRoom = new MovieRoom()
                        {
                            Created = DateTime.Now,
                            CreatedBy = currentUserEmail,
                            Status = OBJECT_STATUS.ENABLE,
                            RoomId = movieRoom.Id,
                        };
                        movieRoomsToCreate.Add(createMovieCinemaRoom);

                        var createMovieTimeSettingList = movieRoom.MovieTimeSettings.Select(e => new MovieTimeSetting()
                        {
                            Created = DateTime.Now,
                            CreatedBy = currentUserEmail,
                            Status = OBJECT_STATUS.ENABLE,
                            Time = e.Time,
                            MovieRoomId = createMovieCinemaRoom.Id
                        });
                        movieTimeSettingsToCreate.AddRange(createMovieTimeSettingList);
                    }
                }
            }


            AddRange(movieDateSettingsToCreate);
            _movieCinema.AddRange(movieCinemasToCreate);
            _movieRoom.AddRange(movieRoomsToCreate);
            _movieTimeSetting.AddRange(movieTimeSettingsToCreate);
        }
    }
}