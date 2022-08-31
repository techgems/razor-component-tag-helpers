# Server Components

## Why server components?

The motivation behind this code sample came from multiple places. 

1) I was working in an application where we needed to use MVC due to a restriction of the customer.

2) I wanted some kind of UI composition and using partials everywhere wasn't working for me since having to write the relative path of the razor view was a chore every time. It also wasn't clear what the partial was unless you knew the file, which made me give an explanation everytime to the other developer in my team that were following my lead.

3) Even after I discovered ViewComponents, I noticed the lacked a specific feature that every client side UI library has: The ability to have child content.

Because of this, I slowly but surely polished an MVC only (no Blazor) approach to creating server rendered components with no lifecycle.

The key was to use Tag Helpers, but to extend them with the ability to render razor views and to render children in the view without having to call @Html.Raw() every time.

If I were to give an answer to [this](https://stackoverflow.com/questions/55206787/viewcomponents-with-children/71130954#71130954) stack overflow question today, I would point them to this repository.

This is the short version of it, but you can read more about this approach in my blog post [here](https://techgems.net/posts/2022/2022-08-31-server-components-with-razor-views/).