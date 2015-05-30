由此项目得出的结论：
使用Modern OpenGL（VBO＋Shader）时，顶点数组(Vertex Array，不是VAO)就不灵了。但是Legacy OpenGL不受影响，该显示什么还显示什么。
所以，SimpleUI部分暂时改为完全由Legacy OpenGL进行渲染。
