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

        public void CreateMovieDateSettings(List<MovieDateSetting> movieDateSettings, long movieId, string currentUserEmail)
        {
            foreach (var movieDateSetting in movieDateSettings)
            {
                var createMovieDateSetting = new MovieDateSetting()
                {
                    Created = DateTime.Now,
                    CreatedBy = currentUserEmail,
                    Status = OBJECT_STATUS.ENABLE,
                    Time = movieDateSetting.Time,
                    MovieId = movieId
                };
                
                Add(createMovieDateSetting);

                foreach (var movieCinema in movieDateSetting.MovieCinemas.ToList())
                {
                    var createMovieCinema = new MovieCinema()
                    {
                        Created = DateTime.Now,
                        CreatedBy = currentUserEmail,
                        Status = OBJECT_STATUS.ENABLE,
                        MovieDateSettingId = createMovieDateSetting.Id,
                        CinemaId = movieCinema.CinemaId
                    };
                    _movieCinema.Add(createMovieCinema);

                    foreach (var movieRoom in movieCinema.MovieRooms)
                    {
                        var createMovieCinemaRoom = new MovieRoom()
                        {
                            Created = DateTime.Now,
                            CreatedBy = currentUserEmail,
                            Status = OBJECT_STATUS.ENABLE,
                            MovieCinemaId = createMovieCinema.Id,
                            RoomId = movieRoom.RoomId
                        };
                        _movieRoom.Add(createMovieCinemaRoom);

                        var createMovieTimeSettingList = movieRoom.MovieTimeSettings.Select(e =>
                        {
                            DateTimeOffset dateTimeOffset = DateTimeOffset.Parse(e.Time);

                            TimeSpan timeSpan = dateTimeOffset.TimeOfDay;

                            string formattedTime = timeSpan.ToString("hh\\:mm");
                            
                            return new MovieTimeSetting()
                            {
                                Created = DateTime.Now,
                                CreatedBy = currentUserEmail,
                                Status = OBJECT_STATUS.ENABLE,
                                Time = formattedTime,
                                Price = e.Price,
                                MovieRoomId = createMovieCinemaRoom.Id
                            };
                        });
                        _movieTimeSetting.AddRange(createMovieTimeSettingList.ToList());
                    }
                }
            }
        }
    }
}