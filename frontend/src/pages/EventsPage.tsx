import { useState, useEffect } from "react";
import type { Event } from "../types";
import './EventsPage.css'

function EventsPage(){
    type FilterValue = 'all' | 'Online' | 'Offline';
    const [filter, setFilter] = useState<FilterValue>('all');
    const [events, setEvents] = useState<Event[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchEvents = async () => {
            try{
                const url = `http://localhost:5000/api/events${filter !== 'all' ? '?type=' + filter : ''}`;
                const response = await fetch(url);

                if(!response.ok){
                    throw new Error('Ошибка загрузки данных');
                }
                const data = await response.json();

                setEvents(data);
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

        fetchEvents();
    }, [filter]);

    if (loading) return <div style={{padding: '20px'}}>Загрузка событий...</div>
    if (error) return <div style={{padding: '20px', color: 'red' }}>Ошибка {error}</div>

    return(
        <div style={{padding: '20px', fontFamily: 'Arial, sans-serif'}}>
            <div style={{display: "flex", flexDirection: 'row', gap: '10px'}}>
                <button
                    onClick={() => setFilter('all')}
                    className={filter === 'all' ? 'filter-btn active' : 'filter-btn'}>
                    Все
                </button>
                <button
                    onClick={() => setFilter('Online')}
                    className={filter === 'Online' ? 'filter-btn active' : 'filter-btn'}>
                    Онлайн
                </button>
                <button
                    onClick={() => setFilter('Offline')}
                    className={filter === 'Offline' ? 'filter-btn active' : 'filter-btn'}>
                    Оффлайн
                </button>
            </div>
            <h1>События</h1>
            {
                events.length === 0 ? ( 
                    filter === 'all' 
                    ? <p>Событий пока нет</p>
                    : <p>По этому фильтру ничего не найдено</p>
                ) : (
                    <ul style={{listStyleType: 'none', padding: 0}}>
                        {events.map(event => (
                            <li key={event.id} style={{border: '1px solid #ccc', borderRadius: '8px', padding: '15px', marginBottom: '10px'}}>
                                <h3>{event.name}</h3>
                                <p>Тип: {event.type}</p>
                                <p>Начало: {new Date(event.startDate).toLocaleDateString('ru-RU')}</p>
                                <p>Локация: {event.location ?? 'онлайн'}</p>
                                {event.ticketPrice !== null && <p>Цена: {event.ticketPrice} ₽</p>}
                                {event.type === 'Offline' && <button>Купить билет</button>}
                            </li>
                        ))}
                    </ul>
                )
            }
        </div>
    );
}

export default EventsPage;