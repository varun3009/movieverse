Feature: Producers

@AddProducer
Scenario: Add an Producer
	Given I am a client
	When I am making a post request to 'Producers' with the following Data '{"Name":"varun","Dob":"2002-09-20","Sex":"male","Bio":"i dont know"}'
	Then response code must be '201'

@AddInvalidProducer
Scenario Outline: Add an Invalid Producer
	Given I am a client
	When I am making a post request to 'Producers' with the following Data '{"Name":"<Name>","Dob":<Dob>,"Sex":"<Sex>","Bio":"<Bio>"}'
	Then response code must be '400'
	And response data must look like '<Error>'
Examples: 
	| Name   | Dob          | Sex  | Bio               | Error             |
	| Varun1 | "2002-09-20" | Male | good Producer     | Name is not valid |
	|        | "2002-09-20" | Male | good Producer     | Name is empty     |
	| Varun  | "2002-09-20" | Male |                   | Bio is empty      |
	| Varun  | "2002-09-20" |      | good Producer     | Sex is empty      |
	| Varun  | "2025-09-20" | Male | good Producer     | Dob is not valid  |
	| Varun  | "2002-09-20" | Male | good Producer1234 | Bio is not valid  |

Scenario Outline:Get an Producer
	Given I am a client
	When I make GET Request 'Producers/<id>'
	Then response code must be '200'
	And  response data must look like '<res>'
Examples: 
	| id | res                                                                      |
	| 1  | {"id":1,"name":"A1","bio":"--","sex":"Male","dob":"2002-09-09T00:00:00"} |
	| 2  | {"id":2,"name":"A2","bio":"--","sex":"Male","dob":"2002-09-10T00:00:00"} |
	#I make GET Request '(.*)'

Scenario:Get all Producers
	Given I am a client
	When I make GET Request 'Producers/'
	Then response code must be '200'
	And response data must look like '[{"id":1,"name":"A1","bio":"--","sex":"Male","dob":"2002-09-09T00:00:00"},{"id":2,"name":"A2","bio":"--","sex":"Male","dob":"2002-09-10T00:00:00"}]'

Scenario:Get an invalid producer
	Given I am a client
	When I make GET Request 'producers/0'
	Then response code must be '404'
	And  response data must look like 'Producer with given id doesn't exist'

Scenario:update an Producer
	Given I am a client
	When I make PUT Request 'Producers/1' with the following Data with the following Data '{"Name":"varun","Dob":"2002-09-20","Sex":"male","Bio":"i dont know"}'
	Then response code must be '200'
	
Scenario:update an invalid Producer
	Given I am a client
	When I make PUT Request 'Producers/0' with the following Data with the following Data '{"Name":"varun","Dob":"2002-09-20","Sex":"male","Bio":"i dont know"}'
	Then response code must be '404'
	And response data must look like 'Producer with given id doesn't exist'

Scenario:update an Producer with invalid details
	Given I am a client
	When I make PUT Request 'Producers/1' with the following Data with the following Data '{"Name":"<Name>","Dob":<Dob>,"Sex":"<Sex>","Bio":"<Bio>"}'
	Then response code must be '400'
	And response data must look like '<Error>'
Examples: 
	| Name   | Dob          | Sex  | Bio               | Error             |
	| Varun1 | "2002-09-20" | Male | good Producer     | Name is not valid |
	|        | "2002-09-20" | Male | good Producer     | Name is empty     |
	| Varun  | "2002-09-20" | Male |                   | Bio is empty      |
	| Varun  | "2002-09-20" |      | good Producer     | Sex is empty      |
	| Varun  | "2025-09-20" | Male | good Producer     | Dob is not valid  |
	| Varun  | "2002-09-20" | Male | good Producer1234 | Bio is not valid  |

Scenario: delete an Producer
	Given I am a client
	When I make Delete Request '/Producers/1'
	Then response code must be '200'

Scenario: delete an invalid Producer
	Given I am a client
	When I make Delete Request '/Producers/0'
	Then response code must be '404'
	And response data must look like 'Producer with given id doesn't exist'
