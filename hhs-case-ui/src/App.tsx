import React, { useEffect, useState } from "react";
import { api } from "./api.ts";
import type { Case } from "./types.ts";

function App() {
    const [cases, setCases] = useState<Case[]>([]);

    useEffect(() => {
        api.get("/Case")
            .then(res => setCases(res.data))
            .catch(err => console.error("Error fetching cases", err));
    }, []);

    const [NewCase, setNewCase] = useState({
        title: "",
        description: "",
        department: "",
        status: "Open"
    });

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        await api.post("/Case", NewCase);
        window.location.reload();
    }

    useEffect(() => {
        api.get("/Case")
            .then(res => {
                console.log("API DATA:", res.data);
                setCases(res.data);
            })
            .catch(err => console.error(err));
    }, []);

    return (
        <div style={{ padding: "20px" }}>
            <h1>HHS Case Tracker</h1>
            <form onSubmit={handleSubmit}>
                <input placeholder="Title" onChange={e => setNewCase({ ...NewCase, title: (e.target as HTMLInputElement).value })} />
                <input placeholder="Description" onChange={e => setNewCase({ ...NewCase, description: (e.target as HTMLInputElement).value })} />
                <input placeholder="Department" onChange={e => setNewCase({ ...NewCase, department: (e.target as HTMLInputElement).value })} />
                <select onChange={e => setNewCase({ ...NewCase, status: (e.target as HTMLSelectElement).value })}>
                    <option>Open</option>
                    <option>In Progress</option>
                    <option>Resolved</option>
                </select>
                <button type="submit">Create Case</button>
            </form>

            <ul>
                {cases.map(c => (
                    <li key={c.caseId}>
                        <strong>{c.title}</strong> — {c.department} — {c.status}
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default App;