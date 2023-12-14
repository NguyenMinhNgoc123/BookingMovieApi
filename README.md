# BookingMovieApi

https://drive.google.com/file/d/1FvY-zlJ3e20klphcGAPDuNdkKBE35K-1/view?usp=sharing


repo Admin:
https://github.com/NguyenMinhNgoc123/BookingMovieWebAdmin

UPDATE movie_db.Movie
SET
StartDate = COALESCE((SELECT MIN(time) FROM movie_db.MovieDateSetting WHERE movie_db.Movie.Id = movie_db.MovieDateSetting.MovieId), CURRENT_TIMESTAMP),
EndDate = COALESCE((SELECT MAX(time) FROM movie_db.MovieDateSetting WHERE movie_db.Movie.Id = movie_db.MovieDateSetting.MovieId), CURRENT_TIMESTAMP);


cách run api: 
1: mở terminal
2: di chuyển vào : cd BOOKING_MOVIE_ADMIN
3: chạy: dotnet run

cách run web fe
1: npm start