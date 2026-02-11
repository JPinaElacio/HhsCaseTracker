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

    const deleteCase = async (id: number) => {
        await api.delete(`/Case/${id}`);
        setCases(cases.filter(c => c.caseId !== id));
    };

    const [editingCase, setEditingCase] = useState<Case | null>(null);

    const updateCase = async (e: React.FormEvent) => {
        e.preventDefault();
        if (editingCase) return;

        await api.put(`/Case/${editingCase!.caseId}`, editingCase);

        setCases(cases.map(c => c.caseId === editingCase!.caseId ? editingCase! : c));
        setEditingCase(null);
    };

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

            {editingCase && (
        <form onSubmit={updateCase}>
            <h3>Edit Case</h3>
            <input value={editingCase.title} onChange={e => setEditingCase({...editingCase, title: e.target.value})} />
            <input value={editingCase.description} onChange={e => setEditingCase({...editingCase, description: e.target.value})} />
            <input value={editingCase.department} onChange={e => setEditingCase({...editingCase, department: e.target.value})} />
            <select value={editingCase.status} onChange={e => setEditingCase({...editingCase, status: e.target.value})}>
            <option>Open</option>
            <option>In Progress</option>
            <option>Resolved</option>
            </select>
            <button type="submit">Save</button>
            <button type="button" onClick={() => setEditingCase(null)}>Cancel</button>
        </form>
        )}

            <ul>
                {cases.map(c => (
                    <li key={c.caseId}>
                        <strong>{c.title}</strong> — {c.department} — {c.status}
                        <button onClick={() => setEditingCase(c)}>Edit</button>
                        <button onClick={() => deleteCase(c.caseId)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default App;