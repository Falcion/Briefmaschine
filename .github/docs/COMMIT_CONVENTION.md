Comitting conventions
---------------------

For consistency throughout the source code, keep these rules in mind as you are working with project's open source:

- all features or bug fixes must be tested;
- all public API methods must be documented;

Inspired by [Azure](https://github.com/MicrosoftDocs/azure-devops/docs) and [Angular.js](https://github.com/angular/angular) project has very precise rules over how GIT commit messages must be formatted - this format leads to easier to read commit history.

Each commit message consists of a header, a body, and a footer:

```html
<header>
BLANK_LINE
<body>
BLANK_LINE
<footer>
```

And there is some advanced explanation for each param in commit's message structure:

- param `<HEADER>` is mandatory and must conform to commit message header formatting (specified below);
- param `<BODY>` is mandatory for every commit except for commits of documentation purpose: when the body is present it must conform to the commit message body formatting;
- param `<FOOTER>` is optional, the same as others must conform specified formatting;

#### Commit message - header

Construction of header of commit's message is a joint of multiple params which defines entire commit preposition:

```html
Final structure of HEADER param:
<TYPE>[<SCOPE>]: <SUMMARY>
```

- param `<TYPE>` is defined by a range of categories, there is a list of them:\
  - **`BUILD`** - changes that affect the build system or external dependencies (for example GULP or NPM);
  - **`CI`** - changes to CI and workflows configurations files and scripts;
  - **`DOCS`** - changes only about documentation (including repository files like README);
  - **`FEAT`** - new feature in future release;
  - **`PERF`** - code changes improving and working with perfomace preferences;
  - **`REFACTOR`** - code change neither fixes something nor adds a feature;
  - **`REF`** - referencing something entirely new based on old changes (in case of full rework of a repository);
  - **`FIX`** - code changes defining fix of a bugs, crashes and some misimprovements (fast-deploy cases);

- param `<SCOPE>` should be a name of the package affected or a code block (as perceived by the person reading changelog generated from the commit messages);
    > None or empty strings which useful for a test or a refactor changes that are done across all packages;

- param `<SUMMARY>` is a field used for the providing a succint description of the change:
  - use the imperative, present tense just like in default committing convetions of git;
  - don't capitalize first letter just like in the button list;
  - don't enter dot at the end of summary.

#### Commit message - body

Signing the CLA
---------------

Sometimes you can be asked to sign the CLA (or contributor license agreement) before sending the PR: for any code changes to be accepted, the CLA must be signed - its a quick and easy process.

- for individuals:\
    https://cla.developers.google.com/about/google-individual
- for corporations, print, sign and scan with email, fam or mail the given form:\
  https://cla.developers.google.com/about/google-corporate

If you have more than one accounts, or multiple email addresses associated with a single account, you must sign the CLA using the primary email address of the GITHUB account used to author git commits and sending PRs.

Following documents can help with sorting out issues with accounts and multiple email addresses:

- https://help.github.com/articles/setting-your-commit-email-address-in-git/
- https://help.github.com/articles/about-commit-email-addresses/
- https://help.github.com/articles/blocking-command-line-pushes-that-expose-your-personal-email-address/