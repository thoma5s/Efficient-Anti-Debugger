# Efficient Anti-Debugger

Low usage anti-processes function, loops through each running process checking the name and title then adds the process Id to a whitelist so that it isn't re-checked, also checks each process array before continuing to check if the total number of processes has been adjusted, this just ensures that we get a low usage percentage.

Averages about 0-1% usage on the majority of computers. Usage can be lowered further by multiplying the thread sleep amount with little performance loss.

![ScreenShot](https://media.discordapp.net/attachments/880236478091911198/997133784363307038/unknown.png)
