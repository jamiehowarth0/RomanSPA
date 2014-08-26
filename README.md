RomanSPA
========

This is the RomanSPA hybrid ASP.NET MVC and Javascript SPA framework.

Why RomanSPA?
-------------

RomanSPA was inspired by [John Papa's Hot Towel packages](https://www.nuget.org/packages?q=hot+towel "Hot Towel Nuget packages") Hot Towel packages by John Papa.  
After a discussion on the ASP.NET forums, John advised that using AngularJS or KnockoutJS to create a website was something of an anti-pattern, especially as you lost out on SEO.  
Given the prevalence of this style of website (Facebook, Twitter, Just-Eat, Hungryhouse, etc.) amongst startups, I figured it must be possible to allow ASP.NET views to be used both to render server-side content
(for example, when Google or Bing is indexing your site), and then using those same views inside a single-page app to give a rich experience to the visitor.  
RomanSPA is a micro-framework that is the result of my investigations.  
  
So, what does it do?
--------------------

RomanSPA demonstrates how to create MVx websites and then use metadata on your controller actions to share views and models between client and server.  
This lets you serve up one lot of content for server renders (e.g. SEO) and use the same views (with the option of changing content in the view thanks to some HttpContextBase extension methods)
to serve up client-friendly views.  
This opens up lots of possibilities:  
*   Client-side view and model caching;  
*   Progressive enhancement of your website "upgrading" it to a SPA;  

How do I get started?
---------------------

Nuget packages will be published shortly.