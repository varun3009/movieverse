Feature: Movies
@AddActor
Scenario: Add a movie
	Given I am a client
	When I am making a post request to 'Movies' with the following Data '{"Name":"RRR","Plot" :"DADkl","YearOfRelease":"2024","ProducerId":"1","Actors":"1 2","Genres":"1","Poster":"www.google.com"}'
	Then response code must be '201'

@AddInvalidActor
Scenario Outline: Add an Invalid movie
	Given I am a client
	When I am making a post request to 'Movies' with the following Data '{"Name":"<Name>","Plot":"<Plot>","YearOfRelease":"<Yor>","ProducerId":"<Pid>","Actors":"<Aid>","Genres":"<Gid>","Poster":"<Poster>"}'
	Then response code must be '400'
	And response data must look like '<Error>'
Examples: 
	| Name | Plot  | Yor  | Pid | Aid  | Gid | Poster         | Error                                |
	| RR@  | DADkl | 2024 | 1   | 1 2  | 1   | www.google.com | Title is not valid                   |
	|      | DADkl | 2024 | 1   | 1 2  | 1   | www.google.com | Title is Empty                       |
	| RR   | 23    | 2024 | 1   | 1 2  | 1   | www.google.com | Invalid Plot                         |
	| RR   |       | 2024 | 1   | 1 2  | 1   | www.google.com | Plot is Empty                        |
	| RR   | DADkl | 2024 | 5   | 1 2  | 1   | www.google.com | Producer with given id doesn't exist |
	| RR   | DADkl | 2024 | 1   | 1 3  | 1   | www.google.com | Actor with given id doesn't exist    |
	| RR   | DADkl | 2024 | 1   | 1 2a | 1   | www.google.com | Invalid Actors Input                 |
	| RR   | DADkl | 2024 | 1   | 1 2  | 5   | www.google.com | Genre with given id doesn't exist    |
	| RR   | DADkl | 2024 | 1   | 1 2  | 1a  | www.google.com | Invalid Genres Input                 |
	| RR   | DADkl | 2024 | 1   | 1 3  | 1   |                | poster is Empty                      |

Scenario: Get all movies
	Given I am a client
	When I make GET Request 'movies?year=2020'
	Then response code must be '200'
	And  response data must look like '[{"id":2,"name":"RRR2","plot":"DADkl","yearOfRelease":2020,"producer":{"id":2,"name":"A2","bio":"--","sex":"Male","dob":"2002-09-10T00:00:00"},"actors":[{"id":1,"name":"A1","bio":"--","sex":"Male","dob":"2002-09-09T00:00:00"}],"genres":[{"id":1,"name":"A1"}],"poster":"www.google.com"}]'

Scenario: Get a movie by id
	Given I am a client
	When I make GET Request 'movies/2'
	Then response code must be '200'
	And response data must look like '{"id":2,"name":"RRR2","plot":"DADkl","yearOfRelease":2020,"producer":{"id":2,"name":"A2","bio":"--","sex":"Male","dob":"2002-09-10T00:00:00"},"actors":[{"id":1,"name":"A1","bio":"--","sex":"Male","dob":"2002-09-09T00:00:00"}],"genres":[{"id":1,"name":"A1"}],"poster":"www.google.com"}'

Scenario: Get a movie by invalid id
	Given I am a client
	When I make GET Request 'movies/3'
	Then response code must be '404'
	And response data must look like 'Movie with given id doesn't exist'

Scenario:update an invalid movie
	Given I am a client
	When I make PUT Request 'movies/0' with the following Data with the following Data '{"Name":"RRR","Plot" :"DADkl","YearOfRelease":"2024","ProducerId":"1","Actors":"1 2","Genres":"1","Poster":"www.google.com"}'
	Then response code must be '404'
	And response data must look like 'Movie with given id doesn't exist'

Scenario:update movie with invalid details
	Given I am a client
	When I make PUT Request 'movies/1' with the following Data with the following Data '{"Name":"<Name>","Plot":"<Plot>","YearOfRelease":"<Yor>","ProducerId":"<Pid>","Actors":"<Aid>","Genres":"<Gid>","Poster":"<Poster>"}'
	Then response code must be '400'
	And response data must look like '<Error>'
Examples: 
	| Name | Plot  | Yor  | Pid | Aid  | Gid | Poster         | Error                                |
	| RR@  | DADkl | 2024 | 1   | 1 2  | 1   | www.google.com | Title is not valid                   |
	|      | DADkl | 2024 | 1   | 1 2  | 1   | www.google.com | Title is Empty                       |
	| RR   | 23    | 2024 | 1   | 1 2  | 1   | www.google.com | Invalid Plot                         |
	| RR   |       | 2024 | 1   | 1 2  | 1   | www.google.com | Plot is Empty                        |
	| RR   | DADkl | 2024 | 5   | 1 2  | 1   | www.google.com | Producer with given id doesn't exist |
	| RR   | DADkl | 2024 | 1   | 1 3  | 1   | www.google.com | Actor with given id doesn't exist    |
	| RR   | DADkl | 2024 | 1   | 1 2a | 1   | www.google.com | Invalid Actors Input                 |
	| RR   | DADkl | 2024 | 1   | 1 2  | 5   | www.google.com | Genre with given id doesn't exist    |
	| RR   | DADkl | 2024 | 1   | 1 2  | 1a  | www.google.com | Invalid Genres Input                 |
	| RR   | DADkl | 2024 | 1   | 1 3  | 1   |                | poster is Empty                      |

Scenario:update a movie
	Given I am a client
	When I make PUT Request 'movies/1' with the following Data with the following Data '{"Name":"RRR","Plot" :"DADkl","YearOfRelease":"2024","ProducerId":"1","Actors":"1","Genres":"1","Poster":"www.google.com"}'
	Then response code must be '200'

Scenario: delete a movie
	Given I am a client
	When I make Delete Request '/movies/1'
	Then response code must be '200'

Scenario: delete an invalid genre
	Given I am a client
	When I make Delete Request '/movies/3'
	Then response code must be '404'
	And response data must look like 'Movie with given id doesn't exist'

