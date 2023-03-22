Guidelines for contributions
----------------------------

We appreciate community contributions to project or documentation, the following list shows some guiding rules to keep in mind when you're contributing to the software (definition from MIT license):

- **DON'T** surprise us with large pull requests, instead, file an issue and start a discussion so we can agree on a direction before you invest a large amount of time;
- **DON'T** include sample code inline in an push;
- **DO** use a snippet project with code to be embedded in the article;
- **DO** follow these instructions and code of conduct;
- **DO** use the forked repository as the starting point of your work;
- **DO** create a separate branch on your fork before working on the project;
- **DO** follow the [GITHUB flow](https://guides.github.com/introduction/flow/);
- **DO** blog and tweet (or whatever) about your contributions if you like!

Following these guidelines will ensure a better experience for you and for us.

Guidelines for submissions
--------------------------

### Submitting an issue

### Submitting pull request (PR)

### Reviewing pull request

Developer reserves the right not to accept pull requests from community members who haven't been good citizens of the community: such behaviour includes not following COC (or code of conduct) and applies within or outside project's managed channels.

**Addressing review feedback.**

If we ask for changes via code reviews then:

1. make the required updates to the code;
2. re-run the CLI tests and build with no-traverse your version to ensure tests are still passing;
3. create fixup commit and push to your forked repository (this will update your PR):

    ```powershell
    git commit --all --fixup HEAD
    git push
    ```

    For more info on working with fixup commits look for an - [Angular.js](https://github.com/angular/angular/blob/main/docs/FIXUP_COMMITS.md).

**Updating the commit message.**

A reviewer might often suggest changes to