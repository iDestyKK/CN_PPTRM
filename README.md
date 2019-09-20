# CN\_PPTRM

### Version 0.3.0 (Last Updated: 2019/09/20)

## About
CN\_PPTRM (Clara Nguyen's Puyo Puyo Tetris Replay Manager) is a tool which let
users manage their replays outside of Puyo Puyo Tetris. They are free to
export their replays to separate files, or import them back into the save to
replay at any given time. The entire purpose of this tool is to get past the
annoying 50 replay limit that the game gives PC players. In addition, players
can share their replays by uploading them if they wish. Replays are stored in
the `data.bin` file.

This is based off of my [ppt\_replay\_tools](https://github.com/iDestyKK/ppt_replay_tools),
which I also wrote to manage replays in this game. I just wanted a nice GUI to
do the job just as well (and be a bit more convenient).

## Preview
<p align = "center">
	<img src = "/img/preview.png?raw=true"/>
</p>

## What is "dem"?
It stands for "Demo", and I named it that because of how other games (notably
Half-Life, Quake, etc) use [Demo Recording](https://wiki.sourceruns.org/wiki/Demo_Recording)
to record key inputs and capture packets to be able to recreate gameplay at
a later time. As of v0.2.0, these files are also compressed with gzip. They
happen to compress really well... going from 90.6 KB to around 4-5 KB!

## "Why use this when I can use on-screen recorders like Fraps or Shadowplay, then delete the replay?"
Would you prefer a file of **up to 91 KB** that contains everything, can be
replayed at any speed you wish, and can be recorded at a later time if you wish
to put it on YouTube? Yeah, me too! :)

Now, obviously, you're going to have to replay the gameplay and record it to
put clips up on YouTube or other video sites. But it's still nice to have the
original replay file right?

## Format Details
I haven't looked too much into the format of `data.bin`, but figuring out how
replays were stored was as simple as making a clean save, making a replay, and
then comparing changes to the file.

Replays are stored in two parts: `PREP` (replay information) and `DATA` (key
presses, events, etc). Each of these are of a fixed length. Each `PREP`
section takes up exactly 360 bytes, and each `DATA` section takes up
92480 bytes. There is a limit to how large a replay can be. Since they are all
a fixed length, my tool simply jumps exactly to where the data is stored and
dumps the contents to a file. Simple huh?
