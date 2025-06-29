# OpenTK Playground

Greetings fellow dev. This is my OpenTK playground, a place where i write stuff with C# and share them with you so you can use them or something.
If i get on a good progress, i might make a full fledged engine out of it :)

# What tools i use or might use:
- OpenTK (The Best C# Wrapper for OpenGL, OpenAL and OpenCL)
- StbiSharp (C# Wrapper for the Stbi Image library - might change it for a C# Wrapped FreeImage library in the future)

# Day 0 - 5/5/2025
![image](https://github.com/user-attachments/assets/6825fd08-b574-4b2e-aa4c-d84908fb5c48)

# Day 1 - 5/6/2025 | Part 1.
![image](https://github.com/user-attachments/assets/aa3b1863-fdb6-4c36-acbb-eeccc5d0e984)
A first mesh was born. His name is Billy the Triangle.

# Day 1 - 5/6/2025 | Part 2.
![image](https://github.com/user-attachments/assets/4d67f694-6b05-4308-9848-67c55113fd33)
Billy grew up quickly and became a somewhat Square.
Also a mention that i expanded the Shader Program code with some Memory Leaks check so i am 100% safe if something goes wrong since my somewhat good checks check everything for me :D
Not only that but the Shader also has some methods for messing around with GLSL uniforms, so this will come in handy when making an MVP (Model * View * Projection) calculation. Off a good start! Thats how i like it.
Also heard of a thing called BufferSubData function, might try it out to see what works and not. might come in handy for Vertex Positions, Normals and UVs.

# Day 3 - 5/7/2025 | Part 1.
![image](https://github.com/user-attachments/assets/fa296814-d16c-4c29-8d92-4a03702b76c5)
Billy became a full blown cube and was finally shifted into the realm of 3D. The cube is optimized since it uses only 8 vertices and 36 indices.
I also prepaired some unfinished UV coordinates for next magic. You can see the code on how i made this madness. It took some trial and error but i manaed to fix a memory leak and some MVP transformation invalidities.
Might also explain why i had such trouble making MVP calculations in my previous OpenGL Attempts. This time i have done it right. Somehow :D

# Day 3 - 5/7/2025 | Part 2.
![image](https://github.com/user-attachments/assets/368e64a1-18e3-4288-8dba-f1e32389dce8)
Billy became a ghostly thin rectangular shape temporarily. I created a basic Transform struct that hold the Positon, Rotation (in euler angles) and Scale. The Transform component calculates the model matrix by a getter making it a simple to approach for the MVP calculation. Later on, we will get moving and get in control with the camera.

# Big Stutter | PC Reset
Hello fellow devs. It's been a time since i posted on this repo. The thing was that i reset my PC due to bad performance. Now, i am back at it again.
![image](https://github.com/user-attachments/assets/2edd3d26-6c6d-4817-98fe-dfc8430d91bf)

# Day 4 - 5/29/2025
![image](https://github.com/user-attachments/assets/d62f950f-e7d7-417d-bc50-8321068eeba1)
Billy's origin has been somehow tampered with as i was playing around and seeing how i could implement Transform parenting and deparenting like the Unity Engine does. Yet i have revamped the whole Window Activity class and added a Window Activity Handler which handles the current Window Activity that has been told to load and keep before removal and/or switching to a next Activity. Pretty convenient if you ask me. I also moved the Cube data into a separate class. In the future i will have a Mesh struct that can have custom vertex data or predefined data like the Cube for example. Next day i will implement the debug camera so we can around the scenery a little.

# Day 6 - 6/25/2025
![image](https://github.com/user-attachments/assets/a98c7911-6ba7-4633-a77e-22e8415feeb7)
I've added and changed some major things. Firstly i made the **Shader.cs** class halfly "abstract" simplifying the process of adding custom/default shaders, making it simpler to use and better to troubleshoot after!
Now onto the biggest change yet. I abstracted the VAO and VBO declaring into these classes **VertexBook.cs**: basically the VAO container; and then **VertexBuffer.cs** that stores all the attributes, data and so on.
The **VertexBook.cs** handles the VBO's automatically so there is no need for long code, it is all godly abstracted. Since **VertexBuffer.cs** has a maority of chained methods, making VBO declaration within **VertexBook.cs** really easy. I had some troubles using for loops with List<???> and such, but then my best course was to use the foreach loops. See the code for yourself and comment or react to it, give me some tips afterwards on how i could improve it. But i think i am off to a good continuation since last time. I still have the Camera movement to do but that will be done once a GFX manager is in place that handles the View and Projection matrices from the Camera - giving absolute flexibility and easy of use.
