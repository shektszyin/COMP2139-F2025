function loadComments(projectId) {
    fetch(`/ProjectManagement/Comments/GetComments/${projectId}`)
        .then(response => response.json())
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
                        <p>${c.Content}</p>
                        <small class="text-muted">${c.CreatedDate}</small>
                    </div>
                `;
            });
        });
}

function setupAddComment(projectId) {
    document.getElementById("addCommentBtn").addEventListener("click", () => {
        const content = document.getElementById("newComment").value.trim();

        if (!content) {
            alert("Comment cannot be empty.");
            return;
        }

        fetch(`/ProjectManagement/Comments/AddComment`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                projectId: projectId,
                content: content
            })
        })
            .then(response => response.json())
            .then(() => {
                document.getElementById("newComment").value = "";
                loadComments(projectId);
            });
    });
}
