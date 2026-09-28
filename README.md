# Razor View Drill — ASP.NET Core MVC

> A practice project for learning **Razor View** in **ASP.NET Core MVC**.

---

## Table of Contents

1. [Overview](#overview)
2. [What the Page Shows](#what-the-page-shows)
3. [Project Structure](#project-structure)
4. [Understanding Razor View](#understanding-razor-view)
5. [How the Application Works](#how-the-application-works)
6. [Build the Drill Step by Step](#build-the-drill-step-by-step)
7. [Razor Syntax Rules Used in the Drill](#razor-syntax-rules-used-in-the-drill)
8. [How foreach Works](#how-foreach-works)
9. [Resources](#resources)

---

## Overview

Razor View Drill demonstrates how data moves from a C# Controller to a Razor View and how C# code can be used together with HTML.

### Concepts Covered
1. **Data Binding:** Using @model to receive data from the Controller and display it in the Razor View.
2. **Loops:** Using @foreach to display items from a list.
3. **Conditions:** Using @if / else to show different content based on a condition.
4. **Dynamic Data:** Using Razor expressions like @DateTime.Now to display current information.
5. **Using Partial** Views to create reusable parts of a webpage.
6. **Layout:** Using a shared Layout to keep common parts like the navbar and footer in one place.
7. **Tag Helpers:** Using asp-controller and asp-action to create links between different pages.


<img width="1917" height="1078" alt="Screenshot 2026-09-28 111216" src="https://github.com/user-attachments/assets/a38b04ee-8e41-4252-a4b1-a603b423129b" />

---

## What the Page Shows

The home page (`Index.cshtml`) shows:

- A heading: **Razor View Drill**
- An **employee table** with serial numbers, made with `@foreach`
- A message: *You have 4 Employee in the list.*, made with `@if / else`
- The **live date and time**, made with `@DateTime.Now`
- A **Refresh List** link, made with a Tag Helper
- A **grey box** with the total count, made with a Partial View
- A **console message** from a Section (open the browser console with `F12`)

---

## Project Structure

Only the files used in this drill are shown:

```
MyFirstMvcApp/
├── Controllers/
│   └── HomeController.cs            → sends the list to the View
└── Views/
    ├── Home/
    │   ├── Index.cshtml             → the drill page
    │   └── _EmployeeCount.cshtml    → the partial view
    └── Shared/
        └── _Layout.cshtml           → the master page (already exists)
```

| File | Job |
|---|---|
| `HomeController.cs` | Makes the list of names and gives it to the View |
| `Index.cshtml` | Shows the table, messages, link and script |
| `_EmployeeCount.cshtml` | A small reusable box that shows the total |
| `_Layout.cshtml` | The shared header, footer and scripts for every page |

The default route in `Program.cs` decides which Controller runs:

```csharp
pattern: "{controller=Home}/{action=Index}/{id?}"
```

The URL `/` has no controller or action, so MVC uses the defaults: `HomeController` and `Index`.

---

## Understanding Razor View

### What is Razor?

**Razor** lets you write **C# code inside an HTML file**. You use the `@` symbol to say *"C# starts here"*.

Razor files end with `.cshtml`:

```
cshtml = C# + HTML
```

### What is a Razor View?

A **Razor View** is a `.cshtml` file in the `Views` folder. Its job is to **show the page** to the user.

```html
<p>Today is @DateTime.Now.ToShortDateString()</p>
```

Razor runs the C# on the **server**. The browser only gets plain HTML:

```html
<p>Today is 9/28/2026</p>
```

> [!IMPORTANT]
> Razor runs on the **server**. The browser never sees your C# code. It only receives HTML.

### Why do we use it?

| Plain HTML | Razor View |
|---|---|
| Same content every time | Content changes with your data |
| Cannot loop over a list | Can loop with `@foreach` |
| Cannot use conditions | Can use `@if` |
| Header and footer copied to every page | One shared `_Layout.cshtml` |

---

## How the Application Works

The drill follows the **MVC** pattern:

| Part | Job | In this drill |
|---|---|---|
| **Model** | The data | A `List<string>` of names |
| **View** | Shows the data as a page | `Index.cshtml` |
| **Controller** | Gets the data and chooses the View | `HomeController.Index()` |

The flow, step by step:

1. The browser asks for `/`.
2. **Routing** finds `HomeController.Index()`.
3. The Controller makes the list and calls `return View(names);`.
4. Razor opens `Views/Home/Index.cshtml`. The list arrives as `Model`.
5. Razor runs your C# and builds plain HTML.
6. The browser shows the page.

---

## Build the Drill Step by Step

### Step 1: Update the Controller

Open `Controllers/HomeController.cs` and change **only** the `Index` action:

```csharp
public IActionResult Index()
{
    var names = new List<string> { "Rakibul Hasan Pranto", "S. Rahman", "Zaman", "Solaiman Shakil" };
    return View(names);
}
```

`return View(names);` does **two things**:

1. It opens `Views/Home/Index.cshtml`.
2. It gives `names` to that View.

### Step 2: Write the View

Replace `Views/Home/Index.cshtml` with:

```html
@model List<string>

@* Razor Syntax 01: @model directive (first line above) - receives List<string> from the Controller *@

@* Razor Syntax 02: Razor comment - this line is not sent to the browser *@

@* Razor Syntax 03: Code block *@
@{
    ViewData["Title"] = "Home Page";
}

<div class="text-center">

    <h1 class="display-4">Razor View Drill</h1>

    @* Razor Syntax 04: @foreach loop with a counter *@
    <h4>Employee List</h4>

    <table class="table table-bordered w-50 mx-auto">
        <thead>
            <tr>
                <th>Serial No.</th>
                <th>Name</th>
            </tr>
        </thead>
        <tbody>
            @{ int i = 1; }
            @foreach (var name in Model)
            {
                <tr>
                    <td>@i</td>
                    <td>@name</td>
                </tr>
                i++;
            }
        </tbody>
    </table>

    @* Razor Syntax 05: @if / else *@
    @if (Model.Count > 0)
    {
        <p>You have @Model.Count Employee in the list.</p>
    }
    else
    {
        <p>No Employee added yet.</p>
    }

    @* Razor Syntax 06: Inline expression *@
    <p>
        Today's date and time is:
        <strong>@DateTime.Now</strong>
    </p>

    @* Razor Syntax 07: Tag Helper (asp-controller and asp-action) *@
    <p>
        <a asp-controller="Home" asp-action="Index">Refresh List</a>
    </p>

    <hr />

    @* Razor Syntax 08: Partial View *@
    <partial name="_EmployeeCount" model="Model" />

</div>

@* Razor Syntax 09: @section sends this script to the Layout *@
@section Scripts {
    <script>
        console.log("Employee count on this page: @Model.Count");
    </script>
}
```

### Step 3: Create the Partial View

Create a new file `Views/Home/_EmployeeCount.cshtml`:

```html
@model List<string>

<div class="alert alert-secondary w-50 mx-auto">
    Total names loaded from controller: <strong>@Model.Count</strong>
</div>
```

> [!NOTE]
> A partial view can be used on many pages. If you change it once, every page that uses it updates.

### Step 4: Run and check

```bash
dotnet watch run
```

You should see:

- The heading **Razor View Drill**
- A table with **4 rows** and serial numbers 1 to 4
- The text **You have 4 Employee in the list.**
- The current date and time (it changes when you refresh)
- A **Refresh List** link
- A grey box: **Total names loaded from controller: 4**

Press `F12` and open the **Console** tab. You will see:

```
Employee count on this page: 4
```

---

## Razor Syntax Rules Used in the Drill

### Quick reference

| # | Rule | Meaning | Where in the drill |
|---|---|---|---|
| 01 | `@model` | Sets the type of data the View receives | First line |
| 02 | `@* *@` | Razor comment | Above each section |
| 03 | `@{ }` | Code block | `ViewData["Title"]` and `int i = 1` |
| 04 | `@foreach` | Loop over a list | Table rows |
| 05 | `@if / else` | Choose what to show | Employee count message |
| 06 | `@` expression | Print a value | `@DateTime.Now`, `@Model.Count` |
| 07 | Tag Helper | Make links with `asp-` attributes | Refresh List link |
| 08 | Partial View | Reuse a small view | `_EmployeeCount.cshtml` |
| 09 | `@section` | Send content to the Layout | Scripts block |

### Syntax 01: @model and Model

`@model` tells the View **what type of data it will receive**. `Model` is the **data itself**.

```html
@model List<string>

<p>Total: @Model.Count</p>
```

| Written as | Meaning |
|---|---|
| `@model` (small **m**) | A directive on the first line. It sets the data type |
| `Model` (big **M**) | The real data from the Controller |

> [!IMPORTANT]
> The type must **match** what the Controller sends. The Controller sends `List<string>`, so the View must say `@model List<string>`.

### Syntax 02: Razor comment

```html
@* This is a Razor comment. It is NOT sent to the browser. *@

<!-- This is an HTML comment. It IS sent to the browser. -->
```

| Type | Written as | Visible in *View Page Source*? |
|---|---|---|
| Razor comment | `@* ... *@` | No |
| HTML comment | `<!-- ... -->` | Yes |

### Syntax 03: Code block

Use `@{ }` to write several lines of C#, for example to make variables.

```html
@{
    ViewData["Title"] = "Home Page";
}
```

- `ViewData["Title"]` sets the page title. `_Layout.cshtml` reads it with `@ViewData["Title"]`.
- The counter `@{ int i = 1; }` in the table is also a code block. It sits **outside** the loop, so it is made only once.

### Syntax 04: @foreach

Goes through a list one item at a time.

```html
@foreach (var name in Model)
{
    <tr>
        <td>@i</td>
        <td>@name</td>
    </tr>
    i++;
}
```

> [!NOTE]
> Inside a loop, plain C# like `i++;` does **not** need `@`. You are already in C# mode. See [How foreach Works](#how-foreach-works) for a full explanation.

### Syntax 05: @if / else

Shows different content depending on a condition.

```html
@if (Model.Count > 0)
{
    <p>You have @Model.Count Employee in the list.</p>
}
else
{
    <p>No Employee added yet.</p>
}
```

- Always use curly braces `{ }`.
- You can write HTML directly inside the braces.
- `else` does not need `@`.

### Syntax 06: Inline expression

`@` followed by a value prints that value.

```html
<strong>@DateTime.Now</strong>
<p>You have @Model.Count Employee</p>
```

If you need math or spaces, use `@( )`:

```html
<p>Next number: @(Model.Count + 1)</p>
```

### Syntax 07: Tag Helper

A **Tag Helper** looks like an HTML attribute and starts with `asp-`. The server turns it into a correct link.

```html
<a asp-controller="Home" asp-action="Index">Refresh List</a>
```

The browser receives:

```html
<a href="/">Refresh List</a>
```

**Why is this good?**

- You do not type the URL by hand.
- If the route changes later, the link still works.

Tag Helpers work because `Views/_ViewImports.cshtml` has this line (it is there by default):

```html
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

### Syntax 08: Partial View

A small `.cshtml` file that you can use again and again.

```html
<partial name="_EmployeeCount" model="Model" />
```

- `name` is the file name, without `.cshtml`.
- `model="Model"` sends the list to the partial.
- The partial must have a matching `@model List<string>` on its first line.
- The `_` at the start of the name is a habit. It tells everyone: *"this is a partial, not a full page"*.
- Razor looks in `Views/Home/` first, then in `Views/Shared/`.

### Syntax 09: @section

A **section** lets a View send extra content (such as a script) to a special place in the Layout.

**In the View** (`Index.cshtml`):

```html
@section Scripts {
    <script>
        console.log("Employee count on this page: @Model.Count");
    </script>
}
```

**In the Layout** (`_Layout.cshtml`, near the bottom, before `</body>`). It is there by default:

```html
@await RenderSectionAsync("Scripts", required: false)
```

`required: false` means a View may skip this section.

### Syntax 10: the Layout and @RenderBody()

`_Layout.cshtml` is the **master page**. It holds the parts every page shares, like the menu, footer and CSS. The content of `Index.cshtml` goes into the Layout at this line:

```html
@RenderBody()
```

That is why you only write the page content in `Index.cshtml`. The header and footer come from the Layout.

---

## How foreach Works

```html
@{ int i = 1; }
@foreach (var name in Model)
{
    <tr>
        <td>@i</td>
        <td>@name</td>
    </tr>
    i++;
}
```

1. `@{ int i = 1; }` makes a counter. It is **outside** the loop. If it were inside, it would reset to 1 in every round.
2. `foreach` takes the items **one by one**. Each item goes into `name`.
3. The code in `{ }` repeats for each item and writes one table row.
4. `i++;` adds 1 to the counter.

| Round | `name` | `i` printed | `i` after `i++` |
|---|---|---|---|
| 1 | Rakibul Hasan Pranto | 1 | 2 |
| 2 | S. Rahman | 2 | 3 |
| 3 | Zaman | 3 | 4 |
| 4 | Solaiman Shakil | 4 | 5 |

After round 4 there are no more items, so the loop stops. The browser only gets the finished rows:

```html
<tbody>
    <tr><td>1</td><td>Rakibul Hasan Pranto</td></tr>
    <tr><td>2</td><td>S. Rahman</td></tr>
    <tr><td>3</td><td>Zaman</td></tr>
    <tr><td>4</td><td>Solaiman Shakil</td></tr>
</tbody>
```

> [!TIP]
> Add a fifth name in the Controller and a fifth row appears. You never touch the HTML.

---

## Resources

- Ravi Patel: [ASP.NET Core .NET 8 Razor Syntax Tutorial for MVC/View](https://medium.com/@ravipatel.it/asp-net-core-net-8-razor-syntax-tutorial-for-mvc-view-d2a85e0979ae)
- Microsoft Learn: [Razor Pages architecture and concepts in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-10.0&tabs=visual-studio)
- Manoj Kalla: [Overview of Razor Views, Razor Pages and Razor Components](https://www.c-sharpcorner.com/article/overview-of-razor-views-razor-pages-and-razor-components)
- Microsoft Learn: [Razor syntax reference for ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-10.0)

---

## About

A project for learning **Razor View** in **ASP.NET Core MVC**: `@model`, comments, code blocks, loops, conditions, Tag Helpers, Partial Views and Sections.

**Topics:** `aspnet-core` · `mvc` · `razor` · `razor-view` · `cshtml` · `csharp`
