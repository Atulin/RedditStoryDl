# Reddit Story DL

*Gotta get those Nature of Predators fanfics to my Kindle somehow*

## Releases

[Download](https://github.com/Atulin/RedditStoryDl/releases/latest)

* `-fd` — framework-dependent, you will need .NET 8 Runtime installed, but the file size is smaller
* `-sc` — self-contained, comes packaged with the Runtime, but the file size is bigger

## Usage

1. Run the program
2. When asked for the URL, paste the link to the first chapter of the series
3. When asked for the directory, press `Enter` to accept the default one, or input your own
4. Wait until everything is downloaded and saved
5. ???
6. PROFIT

## Known issues

* Currently, works only when the chapter has a link named `[Next]` (case-insensitive). Will not work if it's `Next`, `[Next Chapter]`, or anything else.
* The executable size could be better with trimming and Native AOT
