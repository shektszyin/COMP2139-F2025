function loadComments(projectId) {
    fetch(`/ProjectManagement/ProjectComment/GetComments/${projectId}`)
        .then(response => {
            if (!response.ok) throw new Error("Network response was not ok");
            return response.json();
        })
        .then(comments => {
            const section = document.getElementById("comments-section");
            section.innerHTML = "";

            if (comments.length === 0) {
                section.innerHTML = "<p>No comments yet.</p>";
                return;
            }

            comments.forEach(c => {
            
                section.innerHTML += `
                    <div class="border p-2 mb-2">
                        <p>${c.content}</p> 
                        <small class="text-muted">${c.createdDate}</small>
                    </div>
                `;
            });
        })
        .catch(error => console.error('Error:', error));
}

function setupAddComment(projectId) {
    const btn = document.getElementById("addCommentBtn");
    if (!btn) return;

    btn.addEventListener("click", () => {
        const content = document.getElementById("newComment").value.trim();

        if (!content) {
            alert("Comment cannot be empty.");
            return;
        }

       
        fetch(`/ProjectManagement/ProjectComment/AddComment`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                projectId: projectId,
                content: content
            })
        })
        .then(response => response.json())
        .then(data => {
            document.getElementById("newComment").value = "";
            loadComments(projectId);
        })
        .catch(error => console.error('Error:', error));
    });
}