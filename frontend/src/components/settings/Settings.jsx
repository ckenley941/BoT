import Link from '@mui/material/Link';
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";

export default function Settings() 
{
    return (
        <Card variant="outlined"  sx={{ m: 5, maxWidth: 750 }}>
        <CardContent>
        <Link href="/settings/thought-categories">Thought Categories</Link>
      </CardContent>
      </Card>
    );
}

