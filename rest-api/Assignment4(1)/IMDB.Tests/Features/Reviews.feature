Feature: Reviews

A short summary of the feature

@AddGenre
Scenario: Add a review
	Given I am a client
	When I am making a post request to '/movies/1/reviews' with the following Data '{"Message":"varun"}'
	Then response code must be '201'

@AddInvalidGenre
Scenario Outline: Add an Invalid review
	Given I am a client
	When I am making a post request to '/movies/<mid>/reviews' with the following Data '{"Message":"<msg>"}'
	Then response code must be '<code>'
	And response data must look like '<Error>'
Examples: 
	| mid | msg   | Error                             | code |
	| 0   | nice  | Movie with given id doesn't exist | 404  |
	| 1   | nice@ | Message is not valid              | 400  |
	| 1   |       | Message is empty                  | 400  |

Scenario:Get a review
	Given I am a client
	When I make GET Request 'reviews/1'
	Then response code must be '200'
	And  response data must look like '{"id":1,"message":"good","movieId":1}'

Scenario:Get all review
	Given I am a client
	When I make GET Request 'reviews'
	Then response code must be '200'
	And  response data must look like '[{"id":1,"message":"good","movieId":1},{"id":2,"message":"good","movieId":2}]'

Scenario:Get an invalid review
	Given I am a client
	When I make GET Request 'reviews/0'
	Then response code must be '404'
	And  response data must look like 'Review with given id doesn't exist'

Scenario: Get reviews by movie id
	Given I am a client
	When I make GET Request 'movies/1/reviews/'
	Then response code must be '200'
	And  response data must look like '[{"id":1,"message":"good","movieId":1}]'

Scenario: Get reviews by invalid movie id
	Given I am a client
	When I make GET Request 'movies/0/reviews/'
	Then response code must be '404'
	And  response data must look like 'Movie with given id doesn't exist'

Scenario:update a review
	Given I am a client
	When I make PUT Request 'movies/1/reviews/1' with the following Data with the following Data '{"MovieId":"2","Message":"varun"}'
	Then response code must be '200'
	
Scenario:update an invalid review
	Given I am a client
	When I make PUT Request 'movies/1/reviews/0' with the following Data with the following Data '{"MovieId":"1","Message":"varun"}'
	Then response code must be '404'
	And response data must look like 'Review with given id doesn't exist'

Scenario:update a review with invalid details
	Given I am a client
	When I make PUT Request 'movies/1/reviews/1' with the following Data with the following Data '{"MovieId":"<mid>","Message":"<msg>"}'
	Then response code must be '<code>'
	And response data must look like '<Error>'
Examples: 
	| mid | msg   | Error                             | code |
	| 0   | nice  | Movie with given id doesn't exist | 404  |
	| 1   | nice@ | Message is not valid              | 400  |
	| 1   |       | Message is empty                  | 400  |

Scenario: delete a review
	Given I am a client
	When I make Delete Request 'movies/1/reviews/1'
	Then response code must be '200'

Scenario: delete an invalid review
	Given I am a client
	When I make Delete Request 'movies/1/reviews/0'
	Then response code must be '404'
	And response data must look like 'Review with given id doesn't exist'
