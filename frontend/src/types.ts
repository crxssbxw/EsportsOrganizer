export interface Team{
    id: number;
    name: string;
    logoUrl: string | null;
    region: string | null;
}

export interface Event{
    id: number;
    name: string;
    type: 'Online' | 'Offline';
    startDate: string;
    endDate: string;
    location: string | null;
    ticketPrice: number | null;
}

export interface Match{
    id: number;
    event: Event;
    team1: Team;
    team2: Team;
    team1Score: number | null;
    team2Score: number | null;
    matchTime: string;
    status: 'Finished' | 'Live' | 'Scheduled';
}