Feature: Genres

A short summary of the feature

@AddGenre
Scenario: Add an genre
	Given I am a client
	When I am making a post request to 'genres' with the following Data '{"Name":"varun"}'
	Then response code must be '201'

@AddInvalidGenre
Scenario Outline: Add an Invalid genre
	Given I am a client
	When I am making a post request to 'genres' with the following Data '{"Name":"<Name>"}'
	Then response code must be '400'
	And response data must look like '<Error>'
Examples: 
	| Name   |  Error             |
	| Varun1 |  Name is not valid |

Scenario Outline:Get an genre
	Given I am a client
	When I make GET Request 'genres/<id>'
	Then response code must be '200'
	And  response data must look like '<res>'
Examples: 
	| id | res                  |
	| 1  | {"id":1,"name":"A1"} |
	| 2  | {"id":2,"name":"A2"} |

Scenario:Get an invalid genre
	Given I am a client
	When I make GET Request 'genres/0'
	Then response code must be '404'
	And  response data must look like 'Genre with given id doesn't exist'

Scenario:Get all genres
	Given I am a client
	When I make GET Request 'genres/'
	Then response code must be '200'
	And response data must look like '[{"id":1,"name":"A1"},{"id":2,"name":"A2"}]'

Scenario:update an genre
	Given I am a client
	When I make PUT Request 'genres/1' with the following Data with the following Data '{"Name":"varun"}'
	Then response code must be '200'
	
Scenario:update an invalid genre
	Given I am a client
	When I make PUT Request 'genres/0' with the following Data with the following Data '{"Name":"varun"}'
	Then response code must be '404'
	And response data must look like 'Genre with given id doesn't exist'

Scenario:update an genre with invalid details
	Given I am a client
	When I make PUT Request 'genres/1' with the following Data with the following Data '{"Name":"<Name>"}'
	Then response code must be '400'
	And response data must look like '<Error>'
Examples: 
	| Name   | Error             |
	| Varun1 | Name is not valid |

Scenario: delete an genre
	Given I am a client
	When I make Delete Request '/genres/1'
	Then response code must be '200'

Scenario: delete an invalid genre
	Given I am a client
	When I make Delete Request '/genres/0'
	Then response code must be '404'
	And response data must look like 'Genre with given id doesn't exist'