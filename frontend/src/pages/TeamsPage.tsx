import { useEffect, useState } from "react";
import type { Team } from "../types";

function TeamsPage(){
  const [teams, setTeams] = useState<Team[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchTeams = async () => {
      try {
        const response = await fetch('http://localhost:5000/api/teams');
        
        if (!response.ok) {
          throw new Error('Ошибка при загрузке данных');
        }
        
        const data = await response.json(); 
        
        setTeams(data);
      } catch (err) {
            if (err instanceof Error){
                console.error(err);
                setError(err.message);
            }
            else{
                console.error(err);
                setError("Неизвестная ошибка");
            }
      } finally {
        setLoading(false); 
      }
    };

    fetchTeams();
  }, []);

  if (loading) return <div style={{padding: '20px'}}>Загрузка команд...</div>
  if (error) return <div style={{padding: '20px', color: 'red'}}>Ошибка: {error}</div>

  return(
    <div style={{padding: '20px', fontFamily: 'Arial, sans-serif'}}>
      <h1>Команды</h1>
      {
        teams.length === 0 ? (
          <p>Команд пока нет</p>
        ) : (
          <ul style={{listStyleType: 'none', padding: 0}}>
            {teams.map(team => (
              <li key={team.id} style={{
                border: '1px solid #ccc',
                borderRadius: '8px',
                padding: '15px',
                marginBottom: '10px',
                display: 'flex',
                alignItems: 'center',
                gap: '15px'
              }}>
                {team.logoUrl && (
                  <img src={team.logoUrl} alt={team.name} style={{width: '50px', height: '50px', borderRadius: '50%', objectFit: 'cover'}}/>
                )}
                <div>
                  <h3 style={{ margin: 0}}>{team.name}</h3>
                  <p style={{ margin: '5px 0 0 0', color: '#666'}}>Регион {team.region || 'Не указан'}</p>
                </div>
              </li>
            ))}
          </ul>
        )
      }
    </div>
  );
}

export default TeamsPage;