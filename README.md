Brian Murphy Technical Test
===========================

* Did you have time to complete the coding test? What would you add to your solution if you had more time?

	I wasn't told that there was any particular time limit, but at the same time I didn't want to spend an excessive amout of time on this. I had enough time to complete the Stories and pin the behaviour I was asked to implement with tests.
	
	If I spent more time, I'd like to:

	- Give a nicer experience regarding command line parsing/usage text. It's a bit primitive, but does what it needs to.
		
	- Would like to find a *neat* way, with XUnit.ioc, of some tests having the production version of a class and some other tests using a mock version.
		
	- Maybe some of the string formatting in the reporter could be factored out.
	
	- Getting from HttpResponseMessage to the Resource, in RestfulClient, could be factored out.

	- Some configuration stuff could actually be configuration stuff - the BaseAddress, the values of the additional headers, etc.

	- Maybe derive from Tuple<string, string> to give a nicer language for the "additional headers" piece.

* What was the most useful feature in your opinion that was added to C# 4? Please include a snippet of code that shows how you've used it.

	That's a tough one; I'm not a big user of dynamic or the covariance/contravariance goop. That said, I *have* used dynamic and it was pretty nifty. Example:
	
	We had an ApiController that returned search results from Solr. A result could be of many different Types. So, our Resource looked like this:

  	public class SearchResult
  	{
        public dynamic Entity { get; set; }
  	}

	Our AngularJS front-end was then able to inspect the Entity and render it differently depending on what it actually was.

* What's your favourite programming language? Why?

	I like software because I like *making stuff*, so my favourite language is the one that allows me to *make stuff* in the best possible way. I consider myself mostly language/platform/toolset agnostic. Whatever allows me get things done is best.

	For the last few years, I've been most productive in C#, so that gets my vote today.

	I've been writing more and more JavaScript recently, though, and the ability to *make stuff* is just wonderful there (with frameworks like AngularJS, in particular).

	There's something to be said for the simplicity of x86, though!

* How would you track down a performance bottleneck in a .NET application? Have you ever had to do this?

	I've had to spend plenty of time tracking down performance issues in managed and native code over the years, yes.
	
	Step 1: what are the symptoms? The system is slow? Some piece (database/web service) has lodged the CPU at 100%? Your disks are grinding?

	Can we reproduce in a local development environment? 

	Simple stuff. Open Task Manager. What's at the top CPU or memory-wise? Open Resource Monitor. What's your disk queue length look like?

	Know your performance counters. Different technologies (SQL Server, WCF, etc.) are likely to have some useful performance counters to look at. Do you know what you *expect* to see for some of those counters?

	dotTrace is nifty and I've used that a bunch, debugging locally and with some load-testing scripts. I had a weird issue with a REST service last year that dotTrace helped to trace to the Json.NET GUID deserialization code.

	In general, tools will help you to find the hotspots in your system; once you improve one area, your bottle neck moves to the next one. You improve your ASP.NET Web API controller code, but now SQL Server queries are blocking. So you spend a bunch of time in SQL Server Profiler and adding an index works wonders. Next, the disk I/O becomes the bottleneck. And so on.
	
	I'm a fan of running a performance test as part of the build pipeline - that can tell us how system performance is being affected throughout the course of the development cycle.

* How would you improve the Just Eat public API found here?: http://www.just-eat.co.uk/webservice/webservices.asmx

	First thought: from an HTTP service perspetive, these days I expect a service to be RESTful and I expect to speak in the language of Resources.
	So, currently we have these operations:

	- Check to see if restaurant will deliver to postcode.

	GET /webservice/webservices.asmx/DeliveryAreaServerValidation?postcode=string&restaurantId=string&orderType=string

	How about:

	POST /api/Restaurant/{id}/DeliveryArea

	Body:

```json
{
 	Postcode: "W1:
}
```

	Response:

```json
{
	WillDeliver: true
}
```

	- Return all postcodes.

	GET /webservice/webservices.asmx/getAllPostcodes

	RESTfully, this could be:

	GET /api/Postcode

```json
[
 	{ Postcode: W1 },
 	{ Postcode: W2 },
]
```

	- Return data of a specific restaurant a postcode. If postcode is set to 0 the data is fetched for the zipcode of the restaurant.

	GET /webservice/webservices.asmx/getRestaurantData?restaurantId=string&postcode=string

	This is to return the data about a restaurant's operation in a particular postcode? This could be:

	GET /api/Restaurant/{id}/PostcodeData/{postcode}

```json
RestaurantPostcodeResource:
{
	WillDeliver: true,
	AverageWaitTimeMinutes: 30
}
```

	- Return a list of restaurants available for delivery/pickup in a postcode.

	GET /webservice/webservices.asmx/getRestaurantList?postcode=string

	This could be:

	GET /api/Postcode/{postcode}/Restaurant

	Definitely scope to combine these into a more coherent API.

	Final thought: do we have a versioning story? I'm assuming we can't just re-jig things as we please.

* Please describe yourself using either XML or JSON.

	My social network should be able to describe me, so how about...

```json
{
	SocialLinks:
	{
		{
			IdentityProvider: "last.fm",
			Identity: "kiswa"
		},
		{
			IdentityProvider: "LinkedIn",
			Identity: "brainmurphy"
		},
	}
}
```
