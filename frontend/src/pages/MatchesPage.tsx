import { useState, useEffect } from "react";
import type { Match } from "../types";

function MatchesPage(){
    type FilterValue = 'all' | 'Scheduled' | 'Finished' | 'Live';
    const [filter, setFilter] = useState<FilterValue>('all');
    const [matches, setMatches] = useState<Match[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchMatches = async () => {
            try{
                const url = `http://localhost:5000/api/matches${filter !== 'all' ? '?status=' + filter : ''}`;
                const response = await fetch(url);

                if(!response.ok){
                    throw new Error('Ошибка загрузки данных');
                }
                const data = await response.json();

                setMatches(data);
            } catch (err){
                if (err instanceof Error){
                    console.error(err);
                    setError(err.message);
                }
                else{
                    console.error(err);
                    setError("Неизвестная ошибка");
                }
            } finally{
                setLoading(false);
            }
        };

        fetchMatches();
    }, [filter])

    if (loading) return <div style={{padding: '20px'}}>Загрузка матчей...</div>
    if (error) return <div style={{padding: '20px', color: 'red' }}>Ошибка {error}</div>

    return(
        <div style={{padding: '20px', fontFamily: 'Arial, sans-serif'}}>
            <div style={{display: 'flex', flexDirection: 'row', gap: '10px'}}>
                <button
                    onClick={() => setFilter('all')}
                    className={filter === 'all' ? 'filter-btn active' : 'filter-btn'}>
                    Все
                </button>
                <button
                    onClick={() => setFilter('Live')}
                    className={filter === 'Live' ? 'filter-btn active' : 'filter-btn'}>
                    Live
                </button>
                <button
                    onClick={() => setFilter('Scheduled')}
                    className={filter === 'Scheduled' ? 'filter-btn active' : 'filter-btn'}>
                    Запланированные
                </button>       
                <button
                    onClick={() => setFilter('Finished')}
                    className={filter === 'Finished' ? 'filter-btn active' : 'filter-btn'}>
                    Завершенные
                </button>
            </div>
            <h1>Матчи</h1>
            {
                matches.length === 0 ? (
                    filter === 'all' 
                    ? <p>Матчей пока нет</p>
                    : <p>Матчей по этому фильтру не найдено</p>
                ) : (
                    <ul style={{listStyleType: 'none', padding: 0}}>
                        {matches.map(match => (
                            <li key={match.id} style={{border: '1px solid #ccc', borderRadius: '8px', padding: '15px', marginBottom: '10px'}}>
                                <h3>{match.team1.name} vs {match.team2.name}</h3>
                                <p>Турнир: {match.event.name}</p>
                                <p>Время: {new Date(match.matchTime).toLocaleString('ru-RU')}</p>
                                <p>Статус: {match.status}</p>
                                {match.team1Score !== null && match.team2Score !== null ? (
                                    <p>Счёт: {match.team1Score} : {match.team2Score}</p>
                                ) : (
                                    <p>Матч ещё не сыгран</p>
                                )}
                            </li>
                        ))}
                    </ul>
                )
            }
        </div>
    );
}

export default MatchesPage;