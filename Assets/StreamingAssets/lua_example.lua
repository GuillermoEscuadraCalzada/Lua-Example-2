fruits = {
"apple",
"bannana",
}

GetRandomFruit = function()
	return fruits[math.random(1, #fruits)]
end
