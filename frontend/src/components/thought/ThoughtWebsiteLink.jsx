import Grid from "@mui/material/Unstable_Grid2";

export default function ThoughtWebsiteLink({ websiteLink }) 
{
    return (
      <Grid key={websiteLink.websiteLinkId}>{websiteLink.websiteUrl}</Grid>
    );
}

